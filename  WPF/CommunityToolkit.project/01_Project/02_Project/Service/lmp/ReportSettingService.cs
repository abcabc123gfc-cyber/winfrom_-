using _02_Project.DBContext;
using _02_Project.Model.BasicSetting;
using _02_Project.Model.Report;
using Microsoft.EntityFrameworkCore;

namespace _02_Project.Service.lmp
{
    /// <summary>
    /// 报工管理服务实现：工价 / 工艺路线(+明细) / 报工单(+子表单员工)。
    /// 报工单联动取值与自动计算（依据《数据库操作与设计逻辑记录.md》第三章）：
    /// 1) 选工价自动查询 WageSetting 带出 UnitPrice；
    /// 2) Wage = ReportCount × UnitPrice；
    /// 3) EmployeeCount = 子表单 WorkReportEmployee 行数；
    /// 4) 主表与子表单一同保存（同一 DbContext 事务）。
    /// </summary>
    public class ReportSettingService : IReportSettingService
    {
        private readonly IDbContextFactory<SQLContext> _dbFactory;

        public ReportSettingService(IDbContextFactory<SQLContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #region 工价 WageSetting

        public async Task<List<WageSettingDto>> GetWageSettingsAsync(int? id, int? processId, int pageIndex, int pageSize)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.WageSettings.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(w => w.Id == id.Value);
            if (processId.HasValue) query = query.Where(w => w.ProcessId == processId.Value);
            return await query.OrderBy(w => w.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(w => new WageSettingDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    ProcessId = w.ProcessId,
                    ProcessName = w.Process!.Name,
                    UnitPrice = w.UnitPrice,
                    Description = w.Description
                })
                .ToListAsync();
        }

        public async Task<int> GetWageSettingCountAsync(int? id, int? processId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.WageSettings.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(w => w.Id == id.Value);
            if (processId.HasValue) query = query.Where(w => w.ProcessId == processId.Value);
            return await query.CountAsync();
        }

        public async Task<List<OptionDto>> GetWageSettingOptionsAsync(int? processId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.WageSettings.AsNoTracking().AsQueryable();
            if (processId.HasValue) query = query.Where(w => w.ProcessId == processId.Value);
            return await query.OrderBy(w => w.Id)
                .Select(w => new OptionDto { Id = w.Id, Text = w.Name + "（" + w.UnitPrice + "元/件）" })
                .ToListAsync();
        }

        public async Task<AuthResult> AddWageSettingAsync(string name, int processId, decimal unitPrice, string? description, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("工价名称不能为空");
            if (processId <= 0) return AuthResult.Fail("请选择工序");
            if (unitPrice < 0) return AuthResult.Fail("单价不能为负数");

            await using var db = await _dbFactory.CreateDbContextAsync();
            if (!await db.Processes.AnyAsync(p => p.Id == processId))
                return AuthResult.Fail("所选工序不存在");

            db.WageSettings.Add(new WageSetting
            {
                Name = n,
                ProcessId = processId,
                UnitPrice = unitPrice,
                Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim()
            });
            await db.SaveChangesAsync();
            return AuthResult.Ok(0, $"新增工价 [{n}] 成功");
        }

        public async Task<AuthResult> UpdateWageSettingAsync(int id, string name, int processId, decimal unitPrice, string? description, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("工价名称不能为空");
            if (processId <= 0) return AuthResult.Fail("请选择工序");
            if (unitPrice < 0) return AuthResult.Fail("单价不能为负数");

            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.WageSettings.FindAsync(id);
            if (entity == null) return AuthResult.Fail("工价不存在或已被删除");
            if (!await db.Processes.AnyAsync(p => p.Id == processId))
                return AuthResult.Fail("所选工序不存在");

            entity.Name = n;
            entity.ProcessId = processId;
            entity.UnitPrice = unitPrice;
            entity.Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"修改工价 [{n}] 成功");
        }

        public async Task<AuthResult> DeleteWageSettingAsync(int id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.WageSettings.FindAsync(id);
            if (entity == null) return AuthResult.Fail("工价不存在或已被删除");

            // 删除保护：报工单按工价结算，历史记录禁止连带删除
            if (await db.WorkReports.AnyAsync(w => w.WageSettingId == id))
                return AuthResult.Fail($"工价 [{entity.Name}] 已被报工单引用，无法删除（历史工资账目受保护）");

            db.WageSettings.Remove(entity);
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"删除工价 [{entity.Name}] 成功");
        }

        #endregion

        #region 工艺路线 CraftRoute（含工艺流程子表单）

        public async Task<List<CraftRouteDto>> GetCraftRoutesAsync(int? id, int pageIndex, int pageSize)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.CraftRoutes.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(c => c.Id == id.Value);
            return await query.OrderBy(c => c.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(c => new CraftRouteDto
                {
                    Id = c.Id,
                    RouteNo = c.RouteNo,
                    RouteName = c.RouteName,
                    ProductName = c.ProductName,
                    Remark = c.Remark,
                    DetailCount = c.Details.Count
                })
                .ToListAsync();
        }

        public async Task<int> GetCraftRouteCountAsync(int? id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.CraftRoutes.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(c => c.Id == id.Value);
            return await query.CountAsync();
        }

        public async Task<List<OptionDto>> GetProcessOptionsAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Processes.AsNoTracking().OrderBy(p => p.Id)
                .Select(p => new OptionDto { Id = p.Id, Text = p.Name })
                .ToListAsync();
        }

        public async Task<AuthResult> AddCraftRouteAsync(string routeNo, string routeName, string productName, string? remark, List<CraftRouteDetailDto> details, int operatorUserId)
        {
            var no = routeNo?.Trim() ?? string.Empty;
            var name = routeName?.Trim() ?? string.Empty;
            var product = productName?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(no)) return AuthResult.Fail("工艺路线编号不能为空");
            if (string.IsNullOrWhiteSpace(name)) return AuthResult.Fail("工艺名称不能为空");
            if (string.IsNullOrWhiteSpace(product)) return AuthResult.Fail("成品名称不能为空");

            await using var db = await _dbFactory.CreateDbContextAsync();
            if (await db.CraftRoutes.AnyAsync(c => c.RouteNo == no))
                return AuthResult.Fail($"工艺路线编号 [{no}] 已存在");

            var entity = new CraftRoute
            {
                RouteNo = no,
                RouteName = name,
                ProductName = product,
                Remark = string.IsNullOrWhiteSpace(remark) ? null : remark.Trim()
            };
            ApplyDetails(entity, details, db);
            db.CraftRoutes.Add(entity);
            await db.SaveChangesAsync();
            return AuthResult.Ok(entity.Id, $"新增工艺路线 [{no}] 成功，含 {entity.Details.Count} 道工序");
        }

        public async Task<AuthResult> UpdateCraftRouteAsync(int id, string routeNo, string routeName, string productName, string? remark, List<CraftRouteDetailDto> details, int operatorUserId)
        {
            var no = routeNo?.Trim() ?? string.Empty;
            var name = routeName?.Trim() ?? string.Empty;
            var product = productName?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(no)) return AuthResult.Fail("工艺路线编号不能为空");
            if (string.IsNullOrWhiteSpace(name)) return AuthResult.Fail("工艺名称不能为空");
            if (string.IsNullOrWhiteSpace(product)) return AuthResult.Fail("成品名称不能为空");

            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.CraftRoutes.Include(c => c.Details).FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null) return AuthResult.Fail("工艺路线不存在或已被删除");
            if (await db.CraftRoutes.AnyAsync(c => c.RouteNo == no && c.Id != id))
                return AuthResult.Fail($"工艺路线编号 [{no}] 已存在");

            entity.RouteNo = no;
            entity.RouteName = name;
            entity.ProductName = product;
            entity.Remark = string.IsNullOrWhiteSpace(remark) ? null : remark.Trim();

            // 明细整体重建：先清空再按新顺序写入
            db.CraftRouteDetails.RemoveRange(entity.Details);
            entity.Details.Clear();
            ApplyDetails(entity, details, db);

            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"修改工艺路线 [{no}] 成功，含 {entity.Details.Count} 道工序");
        }

        /// <summary>校验明细并生成 CraftRouteDetail 集合（按 SortOrder 排序、工序去重校验）</summary>
        private void ApplyDetails(CraftRoute entity, List<CraftRouteDetailDto>? details, SQLContext db)
        {
            var list = (details ?? new List<CraftRouteDetailDto>())
                .Where(d => d.ProcessId > 0)
                .OrderBy(d => d.SortOrder)
                .ToList();
            // 同一路线不允许重复工序
            var duplicate = list.GroupBy(d => d.ProcessId).FirstOrDefault(g => g.Count() > 1);
            if (duplicate != null)
                throw new InvalidOperationException($"工艺流程中工序重复出现（工序Id={duplicate.Key}），请检查子表单");

            var order = 0;
            foreach (var d in list)
            {
                order++;
                entity.Details.Add(new CraftRouteDetail
                {
                    ProcessId = d.ProcessId,
                    SortOrder = d.SortOrder > 0 ? d.SortOrder : order,
                    Remark = string.IsNullOrWhiteSpace(d.Remark) ? null : d.Remark.Trim()
                });
            }
        }

        public async Task<AuthResult> DeleteCraftRouteAsync(int id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.CraftRoutes.FindAsync(id);
            if (entity == null) return AuthResult.Fail("工艺路线不存在或已被删除");

            db.CraftRoutes.Remove(entity);   // 明细 CraftRouteDetail 由数据库级联删除
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"删除工艺路线 [{entity.RouteNo}] 成功");
        }

        public async Task<List<CraftRouteDetailDto>> GetCraftRouteDetailsAsync(int craftRouteId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.CraftRouteDetails.AsNoTracking()
                .Where(d => d.CraftRouteId == craftRouteId)
                .OrderBy(d => d.SortOrder).ThenBy(d => d.Id)
                .Select(d => new CraftRouteDetailDto
                {
                    ProcessId = d.ProcessId,
                    ProcessName = d.Process!.Name,
                    SortOrder = d.SortOrder,
                    Remark = d.Remark
                })
                .ToListAsync();
        }

        #endregion

        #region 报工单 WorkReport（含子表单员工）

        public async Task<List<WorkReportDto>> GetWorkReportsAsync(int? id, int? employeeId, DateTime? start, DateTime? end, int pageIndex, int pageSize)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.WorkReports.AsNoTracking()
                .Include(w => w.Employee)
                .Include(w => w.WorkGroup)
                .Include(w => w.Process)
                .Include(w => w.WageSetting)
                .Include(w => w.Employees).ThenInclude(wre => wre.Employee)
                .AsQueryable();

            if (id.HasValue) query = query.Where(w => w.Id == id.Value);
            if (employeeId.HasValue) query = query.Where(w => w.EmployeeId == employeeId.Value);
            if (start.HasValue) query = query.Where(w => w.StartTime >= start.Value);
            if (end.HasValue) query = query.Where(w => w.StartTime <= end.Value);

            var list = await query.OrderByDescending(w => w.StartTime)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return list.Select(w => new WorkReportDto
            {
                Id = w.Id,
                StartTime = w.StartTime,
                EndTime = w.EndTime,
                EmployeeId = w.EmployeeId,
                EmployeeName = w.Employee?.Name ?? string.Empty,
                WorkGroupId = w.WorkGroupId,
                WorkGroupName = w.WorkGroup?.Name ?? string.Empty,
                ProcessId = w.ProcessId,
                ProcessName = w.Process?.Name ?? string.Empty,
                WageSettingId = w.WageSettingId,
                WageSettingName = w.WageSetting?.Name ?? string.Empty,
                ReportCount = w.ReportCount,
                UnitPrice = w.UnitPrice,
                Wage = w.Wage,
                EmployeeCount = w.EmployeeCount,
                EmployeeNames = string.Join("、", w.Employees.Select(e => e.Employee?.Name ?? string.Empty)),
                Remark = w.Remark
            }).ToList();
        }

        public async Task<int> GetWorkReportCountAsync(int? id, int? employeeId, DateTime? start, DateTime? end)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.WorkReports.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(w => w.Id == id.Value);
            if (employeeId.HasValue) query = query.Where(w => w.EmployeeId == employeeId.Value);
            if (start.HasValue) query = query.Where(w => w.StartTime >= start.Value);
            if (end.HasValue) query = query.Where(w => w.StartTime <= end.Value);
            return await query.CountAsync();
        }

        public async Task<List<OptionDto>> GetEmployeeOptionsAsync(int? workGroupId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            if (workGroupId.HasValue && workGroupId.Value > 0)
            {
                // 已选工组：仅返回该工组成员（报工子表单按工组过滤）
                return await db.WorkGroupEmployees.AsNoTracking()
                    .Where(wge => wge.WorkGroupId == workGroupId.Value)
                    .OrderBy(wge => wge.EmployeeId)
                    .Select(wge => new OptionDto { Id = wge.EmployeeId, Text = wge.Employee!.Name + "（" + wge.Employee.EmployeeNo + "）" })
                    .ToListAsync();
            }
            return await db.Employees.AsNoTracking().OrderBy(e => e.Id)
                .Select(e => new OptionDto { Id = e.Id, Text = e.Name + "（" + e.EmployeeNo + "）" })
                .ToListAsync();
        }

        public async Task<List<OptionDto>> GetWorkGroupOptionsAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.WorkGroups.AsNoTracking().OrderBy(w => w.Id)
                .Select(w => new OptionDto { Id = w.Id, Text = w.Name })
                .ToListAsync();
        }

        public async Task<List<OptionDto>> GetWorkReportEmployeeOptionsAsync(int workReportId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.WorkReportEmployees.AsNoTracking()
                .Where(wre => wre.WorkReportId == workReportId)
                .OrderBy(wre => wre.EmployeeId)
                .Select(wre => new OptionDto { Id = wre.EmployeeId, Text = wre.Employee!.Name + "（" + wre.Employee.EmployeeNo + "）" })
                .ToListAsync();
        }

        public async Task<AuthResult> AddWorkReportAsync(WorkReportInput input, int operatorUserId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();

            // ===== 校验主表外键 =====
            if (!await db.Employees.AnyAsync(e => e.Id == input.EmployeeId))
                return AuthResult.Fail("所选员工不存在");
            if (!await db.WorkGroups.AnyAsync(g => g.Id == input.WorkGroupId))
                return AuthResult.Fail("所选工组不存在");
            if (!await db.Processes.AnyAsync(p => p.Id == input.ProcessId))
                return AuthResult.Fail("所选工序不存在");

            // ===== 联动取值：工价带出单价 =====
            var wage = await db.WageSettings.FindAsync(input.WageSettingId);
            if (wage == null) return AuthResult.Fail("所选工价不存在");
            if (wage.ProcessId != input.ProcessId)
                return AuthResult.Fail("所选工价与所选工序不匹配，请重新选择");

            if (input.ReportCount < 1) return AuthResult.Fail("报工数量必须大于 0");
            if (input.EndTime < input.StartTime) return AuthResult.Fail("报工结束时间不能早于开始时间");

            // ===== 子表单员工（去重、必须存在） =====
            var employeeIds = (input.EmployeeIds ?? new List<int>()).Distinct().ToList();
            var validIds = await db.Employees.Where(e => employeeIds.Contains(e.Id)).Select(e => e.Id).ToListAsync();
            if (validIds.Count == 0) return AuthResult.Fail("请至少选择 1 名报工员工（子表单[员工姓名]）");

            // ===== 自动计算 =====
            var unitPrice = wage.UnitPrice;
            var wageAmount = input.ReportCount * unitPrice;
            var employeeCount = validIds.Count;

            var report = new WorkReport
            {
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                EmployeeId = input.EmployeeId,
                WorkGroupId = input.WorkGroupId,
                ProcessId = input.ProcessId,
                WageSettingId = input.WageSettingId,
                ReportCount = input.ReportCount,
                UnitPrice = unitPrice,
                Wage = wageAmount,
                EmployeeCount = employeeCount,
                Remark = string.IsNullOrWhiteSpace(input.Remark) ? null : input.Remark.Trim()
            };
            report.Employees = validIds.Select(eid => new WorkReportEmployee { EmployeeId = eid }).ToList();

            db.WorkReports.Add(report);
            await db.SaveChangesAsync();
            return AuthResult.Ok(report.Id, $"报工成功：{input.ReportCount} 件 × {unitPrice:0.##} 元 = {wageAmount:0.##} 元");
        }

        public async Task<AuthResult> UpdateWorkReportAsync(int id, WorkReportInput input, int operatorUserId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var report = await db.WorkReports.Include(w => w.Employees).FirstOrDefaultAsync(w => w.Id == id);
            if (report == null) return AuthResult.Fail("报工单不存在或已被删除");

            if (!await db.Employees.AnyAsync(e => e.Id == input.EmployeeId))
                return AuthResult.Fail("所选员工不存在");
            if (!await db.WorkGroups.AnyAsync(g => g.Id == input.WorkGroupId))
                return AuthResult.Fail("所选工组不存在");
            if (!await db.Processes.AnyAsync(p => p.Id == input.ProcessId))
                return AuthResult.Fail("所选工序不存在");

            var wage = await db.WageSettings.FindAsync(input.WageSettingId);
            if (wage == null) return AuthResult.Fail("所选工价不存在");
            if (wage.ProcessId != input.ProcessId)
                return AuthResult.Fail("所选工价与所选工序不匹配，请重新选择");

            if (input.ReportCount < 1) return AuthResult.Fail("报工数量必须大于 0");
            if (input.EndTime < input.StartTime) return AuthResult.Fail("报工结束时间不能早于开始时间");

            var employeeIds = (input.EmployeeIds ?? new List<int>()).Distinct().ToList();
            var validIds = await db.Employees.Where(e => employeeIds.Contains(e.Id)).Select(e => e.Id).ToListAsync();
            if (validIds.Count == 0) return AuthResult.Fail("请至少选择 1 名报工员工（子表单[员工姓名]）");

            // 主表赋值
            report.StartTime = input.StartTime;
            report.EndTime = input.EndTime;
            report.EmployeeId = input.EmployeeId;
            report.WorkGroupId = input.WorkGroupId;
            report.ProcessId = input.ProcessId;
            report.WageSettingId = input.WageSettingId;
            report.ReportCount = input.ReportCount;
            report.UnitPrice = wage.UnitPrice;
            report.Wage = input.ReportCount * wage.UnitPrice;
            report.EmployeeCount = validIds.Count;
            report.Remark = string.IsNullOrWhiteSpace(input.Remark) ? null : input.Remark.Trim();

            // 子表单整体重建（级联删除旧的再写入新的）
            db.WorkReportEmployees.RemoveRange(report.Employees);
            report.Employees.Clear();
            report.Employees = validIds.Select(eid => new WorkReportEmployee { EmployeeId = eid }).ToList();

            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"修改报工单成功：{report.ReportCount} 件 × {report.UnitPrice:0.##} 元 = {report.Wage:0.##} 元");
        }

        public async Task<AuthResult> DeleteWorkReportAsync(int id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var report = await db.WorkReports.FindAsync(id);
            if (report == null) return AuthResult.Fail("报工单不存在或已被删除");

            db.WorkReports.Remove(report);   // 子表单 WorkReportEmployee 由数据库级联删除
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"删除报工单（Id={id}）成功");
        }

        #endregion
    }
}
