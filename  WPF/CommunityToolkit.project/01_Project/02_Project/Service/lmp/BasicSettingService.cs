using _02_Project.DBContext;
using _02_Project.Model.BasicSetting;
using Microsoft.EntityFrameworkCore;

namespace _02_Project.Service.lmp
{
    /// <summary>
    /// 基础设置服务实现：基于 EF Core 操作 Workshop / ProductionLine / WorkStation / Employee / WorkGroup / Process 表。
    /// 删除保护遵循《数据库操作与设计逻辑记录.md》：
    /// 1) 车间→生产线→工位 保留主级联链路（数据库配置），删除前先做业务检查；
    /// 2) 员工/工序/工价 被报工单（历史账目）引用时禁止物理删除；
    /// 3) 多对多子表单（工组-员工、工序-工位）随主表一起同步。
    /// </summary>
    public class BasicSettingService : IBasicSettingService
    {
        private readonly IDbContextFactory<SQLContext> _dbFactory;

        public BasicSettingService(IDbContextFactory<SQLContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #region 车间 Workshop

        public async Task<List<WorkshopDto>> GetWorkshopsAsync(int? id, int pageIndex, int pageSize)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.Workshops.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(w => w.Id == id.Value);
            return await query.OrderBy(w => w.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(w => new WorkshopDto { Id = w.Id, Name = w.Name })
                .ToListAsync();
        }

        public async Task<int> GetWorkshopCountAsync(int? id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.Workshops.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(w => w.Id == id.Value);
            return await query.CountAsync();
        }

        public async Task<List<OptionDto>> GetWorkshopOptionsAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Workshops.AsNoTracking().OrderBy(w => w.Id)
                .Select(w => new OptionDto { Id = w.Id, Text = w.Name })
                .ToListAsync();
        }

        public async Task<AuthResult> AddWorkshopAsync(string name, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("车间名称不能为空");

            await using var db = await _dbFactory.CreateDbContextAsync();
            if (await db.Workshops.AnyAsync(w => w.Name == n))
                return AuthResult.Fail($"车间 [{n}] 已存在");

            db.Workshops.Add(new Workshop { Name = n });
            await db.SaveChangesAsync();
            return AuthResult.Ok(0, $"新增车间 [{n}] 成功");
        }

        public async Task<AuthResult> UpdateWorkshopAsync(int id, string name, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("车间名称不能为空");

            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.Workshops.FindAsync(id);
            if (entity == null) return AuthResult.Fail("车间不存在或已被删除");
            if (await db.Workshops.AnyAsync(w => w.Name == n && w.Id != id))
                return AuthResult.Fail($"车间 [{n}] 已存在");

            entity.Name = n;
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"修改车间 [{n}] 成功");
        }

        public async Task<AuthResult> DeleteWorkshopAsync(int id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.Workshops.FindAsync(id);
            if (entity == null) return AuthResult.Fail("车间不存在或已被删除");

            // 删除保护：车间下还有生产线/工位时禁止删除（防止误删业务结构）
            if (await db.ProductionLines.AnyAsync(p => p.WorkshopId == id))
                return AuthResult.Fail("该车间下存在生产线，无法删除，请先删除生产线");
            if (await db.WorkStations.AnyAsync(s => s.WorkshopId == id))
                return AuthResult.Fail("该车间下存在工位，无法删除，请先删除工位");

            db.Workshops.Remove(entity);
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"删除车间 [{entity.Name}] 成功");
        }

        #endregion

        #region 生产线 ProductionLine

        public async Task<List<ProductionLineDto>> GetProductionLinesAsync(int? id, int? workshopId, int pageIndex, int pageSize)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.ProductionLines.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(p => p.Id == id.Value);
            if (workshopId.HasValue) query = query.Where(p => p.WorkshopId == workshopId.Value);
            return await query.OrderBy(p => p.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(p => new ProductionLineDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    WorkshopId = p.WorkshopId,
                    WorkshopName = p.Workshop!.Name
                })
                .ToListAsync();
        }

        public async Task<int> GetProductionLineCountAsync(int? id, int? workshopId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.ProductionLines.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(p => p.Id == id.Value);
            if (workshopId.HasValue) query = query.Where(p => p.WorkshopId == workshopId.Value);
            return await query.CountAsync();
        }

        public async Task<List<OptionDto>> GetProductionLineOptionsAsync(int? workshopId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.ProductionLines.AsNoTracking().AsQueryable();
            if (workshopId.HasValue) query = query.Where(p => p.WorkshopId == workshopId.Value);
            return await query.OrderBy(p => p.Id)
                .Select(p => new OptionDto { Id = p.Id, Text = p.Name })
                .ToListAsync();
        }

        public async Task<AuthResult> AddProductionLineAsync(string name, int workshopId, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("生产线名称不能为空");
            if (workshopId <= 0) return AuthResult.Fail("请选择所属车间");

            await using var db = await _dbFactory.CreateDbContextAsync();
            if (!await db.Workshops.AnyAsync(w => w.Id == workshopId))
                return AuthResult.Fail("所选车间不存在");
            if (await db.ProductionLines.AnyAsync(p => p.Name == n && p.WorkshopId == workshopId))
                return AuthResult.Fail($"该车间下生产线 [{n}] 已存在");

            db.ProductionLines.Add(new ProductionLine { Name = n, WorkshopId = workshopId });
            await db.SaveChangesAsync();
            return AuthResult.Ok(0, $"新增生产线 [{n}] 成功");
        }

        public async Task<AuthResult> UpdateProductionLineAsync(int id, string name, int workshopId, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("生产线名称不能为空");
            if (workshopId <= 0) return AuthResult.Fail("请选择所属车间");

            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.ProductionLines.FindAsync(id);
            if (entity == null) return AuthResult.Fail("生产线不存在或已被删除");
            if (!await db.Workshops.AnyAsync(w => w.Id == workshopId))
                return AuthResult.Fail("所选车间不存在");
            if (await db.ProductionLines.AnyAsync(p => p.Name == n && p.WorkshopId == workshopId && p.Id != id))
                return AuthResult.Fail($"该车间下生产线 [{n}] 已存在");

            entity.Name = n;
            entity.WorkshopId = workshopId;
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"修改生产线 [{n}] 成功");
        }

        public async Task<AuthResult> DeleteProductionLineAsync(int id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.ProductionLines.FindAsync(id);
            if (entity == null) return AuthResult.Fail("生产线不存在或已被删除");

            if (await db.WorkStations.AnyAsync(s => s.ProductionLineId == id))
                return AuthResult.Fail("该生产线下存在工位，无法删除，请先删除工位");

            db.ProductionLines.Remove(entity);
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"删除生产线 [{entity.Name}] 成功");
        }

        #endregion

        #region 工位 WorkStation

        public async Task<List<WorkStationDto>> GetWorkStationsAsync(int? id, int? workshopId, int? productionLineId, int pageIndex, int pageSize)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.WorkStations.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(s => s.Id == id.Value);
            if (workshopId.HasValue) query = query.Where(s => s.WorkshopId == workshopId.Value);
            if (productionLineId.HasValue) query = query.Where(s => s.ProductionLineId == productionLineId.Value);
            return await query.OrderBy(s => s.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(s => new WorkStationDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    WorkshopId = s.WorkshopId,
                    WorkshopName = s.Workshop!.Name,
                    ProductionLineId = s.ProductionLineId,
                    ProductionLineName = s.ProductionLine!.Name,
                    Remark = s.Remark
                })
                .ToListAsync();
        }

        public async Task<int> GetWorkStationCountAsync(int? id, int? workshopId, int? productionLineId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.WorkStations.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(s => s.Id == id.Value);
            if (workshopId.HasValue) query = query.Where(s => s.WorkshopId == workshopId.Value);
            if (productionLineId.HasValue) query = query.Where(s => s.ProductionLineId == productionLineId.Value);
            return await query.CountAsync();
        }

        public async Task<List<OptionDto>> GetWorkStationOptionsAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.WorkStations.AsNoTracking().OrderBy(s => s.Id)
                .Select(s => new OptionDto { Id = s.Id, Text = s.Name })
                .ToListAsync();
        }

        public async Task<AuthResult> AddWorkStationAsync(string name, int workshopId, int productionLineId, string? remark, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("工位名称不能为空");
            if (workshopId <= 0) return AuthResult.Fail("请选择所属车间");
            if (productionLineId <= 0) return AuthResult.Fail("请选择所属生产线");

            await using var db = await _dbFactory.CreateDbContextAsync();
            // 生产线必须属于所选车间（防止把生产线挂错车间）
            if (!await db.ProductionLines.AnyAsync(p => p.Id == productionLineId && p.WorkshopId == workshopId))
                return AuthResult.Fail("所选生产线不属于所选车间，请重新选择");

            db.WorkStations.Add(new WorkStation
            {
                Name = n,
                WorkshopId = workshopId,
                ProductionLineId = productionLineId,
                Remark = string.IsNullOrWhiteSpace(remark) ? null : remark.Trim()
            });
            await db.SaveChangesAsync();
            return AuthResult.Ok(0, $"新增工位 [{n}] 成功");
        }

        public async Task<AuthResult> UpdateWorkStationAsync(int id, string name, int workshopId, int productionLineId, string? remark, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("工位名称不能为空");
            if (workshopId <= 0) return AuthResult.Fail("请选择所属车间");
            if (productionLineId <= 0) return AuthResult.Fail("请选择所属生产线");

            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.WorkStations.FindAsync(id);
            if (entity == null) return AuthResult.Fail("工位不存在或已被删除");
            if (!await db.ProductionLines.AnyAsync(p => p.Id == productionLineId && p.WorkshopId == workshopId))
                return AuthResult.Fail("所选生产线不属于所选车间，请重新选择");

            entity.Name = n;
            entity.WorkshopId = workshopId;
            entity.ProductionLineId = productionLineId;
            entity.Remark = string.IsNullOrWhiteSpace(remark) ? null : remark.Trim();
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"修改工位 [{n}] 成功");
        }

        public async Task<AuthResult> DeleteWorkStationAsync(int id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.WorkStations.FindAsync(id);
            if (entity == null) return AuthResult.Fail("工位不存在或已被删除");

            // 工序-工位 外键为 Restrict，工序绑定后不可删除
            if (await db.ProcessWorkStations.AnyAsync(pws => pws.WorkStationId == id))
                return AuthResult.Fail("该工位已被工序绑定，无法删除，请先在工序管理中解除绑定");

            db.WorkStations.Remove(entity);
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"删除工位 [{entity.Name}] 成功");
        }

        #endregion

        #region 员工 Employee

        public async Task<List<EmployeeDto>> GetEmployeesAsync(int? id, int? workshopId, int pageIndex, int pageSize)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.Employees.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(e => e.Id == id.Value);
            if (workshopId.HasValue) query = query.Where(e => e.WorkshopId == workshopId.Value);
            return await query.OrderBy(e => e.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    EmployeeNo = e.EmployeeNo,
                    WorkshopId = e.WorkshopId,
                    WorkshopName = e.Workshop!.Name
                })
                .ToListAsync();
        }

        public async Task<int> GetEmployeeCountAsync(int? id, int? workshopId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.Employees.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(e => e.Id == id.Value);
            if (workshopId.HasValue) query = query.Where(e => e.WorkshopId == workshopId.Value);
            return await query.CountAsync();
        }

        public async Task<List<OptionDto>> GetEmployeeOptionsAsync(int? workshopId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.Employees.AsNoTracking().AsQueryable();
            if (workshopId.HasValue) query = query.Where(e => e.WorkshopId == workshopId.Value);
            return await query.OrderBy(e => e.Id)
                .Select(e => new OptionDto { Id = e.Id, Text = e.Name + "（" + e.EmployeeNo + "）" })
                .ToListAsync();
        }

        public async Task<AuthResult> AddEmployeeAsync(string name, string employeeNo, int workshopId, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            var no = employeeNo?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("员工姓名不能为空");
            if (string.IsNullOrWhiteSpace(no)) return AuthResult.Fail("工号不能为空");
            if (workshopId <= 0) return AuthResult.Fail("请选择所属车间");

            await using var db = await _dbFactory.CreateDbContextAsync();
            if (!await db.Workshops.AnyAsync(w => w.Id == workshopId))
                return AuthResult.Fail("所选车间不存在");
            if (await db.Employees.AnyAsync(e => e.EmployeeNo == no))
                return AuthResult.Fail($"工号 [{no}] 已存在");

            db.Employees.Add(new Employee { Name = n, EmployeeNo = no, WorkshopId = workshopId });
            await db.SaveChangesAsync();
            return AuthResult.Ok(0, $"新增员工 [{n}] 成功");
        }

        public async Task<AuthResult> UpdateEmployeeAsync(int id, string name, string employeeNo, int workshopId, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            var no = employeeNo?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("员工姓名不能为空");
            if (string.IsNullOrWhiteSpace(no)) return AuthResult.Fail("工号不能为空");
            if (workshopId <= 0) return AuthResult.Fail("请选择所属车间");

            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.Employees.FindAsync(id);
            if (entity == null) return AuthResult.Fail("员工不存在或已被删除");
            if (!await db.Workshops.AnyAsync(w => w.Id == workshopId))
                return AuthResult.Fail("所选车间不存在");
            if (await db.Employees.AnyAsync(e => e.EmployeeNo == no && e.Id != id))
                return AuthResult.Fail($"工号 [{no}] 已存在");

            entity.Name = n;
            entity.EmployeeNo = no;
            entity.WorkshopId = workshopId;
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"修改员工 [{n}] 成功");
        }

        public async Task<AuthResult> DeleteEmployeeAsync(int id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.Employees.FindAsync(id);
            if (entity == null) return AuthResult.Fail("员工不存在或已被删除");

            // 删除保护（历史账目）：存在报工记录禁止删除
            if (await db.WorkReports.AnyAsync(w => w.EmployeeId == id))
                return AuthResult.Fail($"员工 [{entity.Name}] 存在报工记录，无法删除（历史工资账目受保护）");
            if (await db.WorkReportEmployees.AnyAsync(wre => wre.EmployeeId == id))
                return AuthResult.Fail($"员工 [{entity.Name}] 存在于报工单子表单中，无法删除");
            if (await db.WorkGroupEmployees.AnyAsync(wge => wge.EmployeeId == id))
                return AuthResult.Fail($"员工 [{entity.Name}] 仍属于工组，请先在工组管理中移除");

            db.Employees.Remove(entity);
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"删除员工 [{entity.Name}] 成功");
        }

        #endregion

        #region 工组 WorkGroup（含 工组-员工 多对多子表单）

        public async Task<List<WorkGroupDto>> GetWorkGroupsAsync(int? id, int pageIndex, int pageSize)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.WorkGroups.AsNoTracking()
                .Include(w => w.Members).ThenInclude(m => m.Employee)
                .AsQueryable();
            if (id.HasValue) query = query.Where(w => w.Id == id.Value);

            var list = await query.OrderBy(w => w.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return list.Select(w => new WorkGroupDto
            {
                Id = w.Id,
                Name = w.Name,
                MemberCount = w.Members.Count,
                MembersText = string.Join("、", w.Members.Select(m => m.Employee?.Name ?? string.Empty))
            }).ToList();
        }

        public async Task<int> GetWorkGroupCountAsync(int? id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.WorkGroups.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(w => w.Id == id.Value);
            return await query.CountAsync();
        }

        public async Task<List<OptionDto>> GetWorkGroupOptionsAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.WorkGroups.AsNoTracking().OrderBy(w => w.Id)
                .Select(w => new OptionDto { Id = w.Id, Text = w.Name })
                .ToListAsync();
        }

        public async Task<AuthResult> AddWorkGroupAsync(string name, List<int> employeeIds, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("工组名称不能为空");

            await using var db = await _dbFactory.CreateDbContextAsync();
            if (await db.WorkGroups.AnyAsync(w => w.Name == n))
                return AuthResult.Fail($"工组 [{n}] 已存在");

            var ids = (employeeIds ?? new List<int>()).Distinct().ToList();
            var valid = await db.Employees.Where(e => ids.Contains(e.Id)).Select(e => e.Id).ToListAsync();
            var entity = new WorkGroup { Name = n };
            entity.Members = valid.Select(eid => new WorkGroupEmployee { EmployeeId = eid }).ToList();
            db.WorkGroups.Add(entity);
            await db.SaveChangesAsync();
            return AuthResult.Ok(entity.Id, $"新增工组 [{n}] 成功，含 {entity.Members.Count} 名员工");
        }

        public async Task<AuthResult> UpdateWorkGroupAsync(int id, string name, List<int> employeeIds, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("工组名称不能为空");

            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.WorkGroups.Include(w => w.Members).FirstOrDefaultAsync(w => w.Id == id);
            if (entity == null) return AuthResult.Fail("工组不存在或已被删除");
            if (await db.WorkGroups.AnyAsync(w => w.Name == n && w.Id != id))
                return AuthResult.Fail($"工组 [{n}] 已存在");

            var ids = (employeeIds ?? new List<int>()).Distinct().ToList();
            var valid = await db.Employees.Where(e => ids.Contains(e.Id)).Select(e => e.Id).ToListAsync();

            // 差量同步成员：只增删变化的部分，不动历史记录
            var removedMembers = entity.Members.Where(m => !valid.Contains(m.EmployeeId)).ToList();
            foreach (var m in removedMembers)
                entity.Members.Remove(m);
            var existing = entity.Members.Select(m => m.EmployeeId).ToHashSet();
            foreach (var eid in valid)
                if (!existing.Contains(eid))
                    entity.Members.Add(new WorkGroupEmployee { EmployeeId = eid });

            entity.Name = n;
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"修改工组 [{n}] 成功，含 {entity.Members.Count} 名员工");
        }

        public async Task<AuthResult> DeleteWorkGroupAsync(int id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.WorkGroups.FindAsync(id);
            if (entity == null) return AuthResult.Fail("工组不存在或已被删除");

            if (await db.WorkReports.AnyAsync(w => w.WorkGroupId == id))
                return AuthResult.Fail($"工组 [{entity.Name}] 存在报工记录，无法删除（历史工资账目受保护）");

            db.WorkGroups.Remove(entity);
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"删除工组 [{entity.Name}] 成功");
        }

        public async Task<List<OptionDto>> GetWorkGroupEmployeeOptionsAsync(int workGroupId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.WorkGroupEmployees.AsNoTracking()
                .Where(wge => wge.WorkGroupId == workGroupId)
                .OrderBy(wge => wge.EmployeeId)
                .Select(wge => new OptionDto { Id = wge.EmployeeId, Text = wge.Employee!.Name + "（" + wge.Employee.EmployeeNo + "）" })
                .ToListAsync();
        }

        #endregion

        #region 工序 Process（含 工序-工位 多对多子表单）

        public async Task<List<ProcessDto>> GetProcessesAsync(int? id, int pageIndex, int pageSize)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.Processes.AsNoTracking()
                .Include(p => p.ProcessWorkStations).ThenInclude(pws => pws.WorkStation)
                .AsQueryable();
            if (id.HasValue) query = query.Where(p => p.Id == id.Value);

            var list = await query.OrderBy(p => p.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return list.Select(p => new ProcessDto
            {
                Id = p.Id,
                Name = p.Name,
                Remark = p.Remark,
                StationCount = p.ProcessWorkStations.Count,
                StationsText = string.Join("、", p.ProcessWorkStations.Select(s => s.WorkStation?.Name ?? string.Empty))
            }).ToList();
        }

        public async Task<int> GetProcessCountAsync(int? id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var query = db.Processes.AsNoTracking().AsQueryable();
            if (id.HasValue) query = query.Where(p => p.Id == id.Value);
            return await query.CountAsync();
        }

        public async Task<List<OptionDto>> GetProcessOptionsAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Processes.AsNoTracking().OrderBy(p => p.Id)
                .Select(p => new OptionDto { Id = p.Id, Text = p.Name })
                .ToListAsync();
        }

        public async Task<AuthResult> AddProcessAsync(string name, string? remark, List<int> stationIds, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("工序名称不能为空");

            await using var db = await _dbFactory.CreateDbContextAsync();
            if (await db.Processes.AnyAsync(p => p.Name == n))
                return AuthResult.Fail($"工序 [{n}] 已存在");

            var ids = (stationIds ?? new List<int>()).Distinct().ToList();
            var valid = await db.WorkStations.Where(s => ids.Contains(s.Id)).Select(s => s.Id).ToListAsync();
            var entity = new Process
            {
                Name = n,
                Remark = string.IsNullOrWhiteSpace(remark) ? null : remark.Trim()
            };
            entity.ProcessWorkStations = valid.Select(sid => new ProcessWorkStation { WorkStationId = sid }).ToList();
            db.Processes.Add(entity);
            await db.SaveChangesAsync();
            return AuthResult.Ok(entity.Id, $"新增工序 [{n}] 成功，绑定 {entity.ProcessWorkStations.Count} 个工位");
        }

        public async Task<AuthResult> UpdateProcessAsync(int id, string name, string? remark, List<int> stationIds, int operatorUserId)
        {
            var n = name?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(n)) return AuthResult.Fail("工序名称不能为空");

            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.Processes.Include(p => p.ProcessWorkStations).FirstOrDefaultAsync(p => p.Id == id);
            if (entity == null) return AuthResult.Fail("工序不存在或已被删除");
            if (await db.Processes.AnyAsync(p => p.Name == n && p.Id != id))
                return AuthResult.Fail($"工序 [{n}] 已存在");

            var ids = (stationIds ?? new List<int>()).Distinct().ToList();
            var valid = await db.WorkStations.Where(s => ids.Contains(s.Id)).Select(s => s.Id).ToListAsync();

            var removedStations = entity.ProcessWorkStations.Where(pws => !valid.Contains(pws.WorkStationId)).ToList();
            foreach (var pws in removedStations)
                entity.ProcessWorkStations.Remove(pws);
            var existing = entity.ProcessWorkStations.Select(pws => pws.WorkStationId).ToHashSet();
            foreach (var sid in valid)
                if (!existing.Contains(sid))
                    entity.ProcessWorkStations.Add(new ProcessWorkStation { WorkStationId = sid });

            entity.Name = n;
            entity.Remark = string.IsNullOrWhiteSpace(remark) ? null : remark.Trim();
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"修改工序 [{n}] 成功，绑定 {entity.ProcessWorkStations.Count} 个工位");
        }

        public async Task<AuthResult> DeleteProcessAsync(int id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var entity = await db.Processes.FindAsync(id);
            if (entity == null) return AuthResult.Fail("工序不存在或已被删除");

            // 删除保护：工序被业务数据引用时禁止物理删除
            if (await db.WorkReports.AnyAsync(w => w.ProcessId == id))
                return AuthResult.Fail($"工序 [{entity.Name}] 存在报工记录，无法删除（历史工资账目受保护）");
            if (await db.WageSettings.AnyAsync(ws => ws.ProcessId == id))
                return AuthResult.Fail($"工序 [{entity.Name}] 已被工价引用，无法删除，请先删除相关工价");
            if (await db.CraftRouteDetails.AnyAsync(cd => cd.ProcessId == id))
                return AuthResult.Fail($"工序 [{entity.Name}] 已被工艺路线引用，无法删除，请先在工艺路线中移除");

            db.Processes.Remove(entity);
            await db.SaveChangesAsync();
            return AuthResult.Ok(id, $"删除工序 [{entity.Name}] 成功");
        }

        public async Task<List<OptionDto>> GetProcessStationOptionsAsync(int processId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.ProcessWorkStations.AsNoTracking()
                .Where(pws => pws.ProcessId == processId)
                .OrderBy(pws => pws.WorkStationId)
                .Select(pws => new OptionDto { Id = pws.WorkStationId, Text = pws.WorkStation!.Name })
                .ToListAsync();
        }

        #endregion
    }
}
