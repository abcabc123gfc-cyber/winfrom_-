using _02_Project.DBContext;
using _02_Project.Model.BasicSetting;
using _02_Project.Model.Report;
using Microsoft.EntityFrameworkCore;

namespace _02_Project.Service.lmp
{
    /// <summary>
    /// 薪资报表服务实现：
    /// 薪资统计 = 按员工分组累加数量/工资/次数；业绩统计 = 按人员或按工序分组。
    /// 时间过滤口径：报工单 StartTime（报工时间-开始）落在 [start, end] 区间。
    /// </summary>
    public class SalaryReportService : ISalaryReportService
    {
        private readonly IDbContextFactory<SQLContext> _dbFactory;

        public SalaryReportService(IDbContextFactory<SQLContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>构造带时间段/员工/工序过滤的报工单查询（StartTime 口径）</summary>
        private IQueryable<WorkReport> BuildQuery(SQLContext db, DateTime? start, DateTime? end, int? employeeId, int? processId)
        {
            var q = db.WorkReports.AsNoTracking().AsQueryable();
            if (start.HasValue) q = q.Where(w => w.StartTime >= start.Value);
            if (end.HasValue) q = q.Where(w => w.StartTime <= end.Value);
            if (employeeId.HasValue) q = q.Where(w => w.EmployeeId == employeeId.Value);
            if (processId.HasValue) q = q.Where(w => w.ProcessId == processId.Value);
            return q;
        }

        public async Task<List<SalarySummaryDto>> GetSalarySummaryAsync(DateTime? start, DateTime? end, int? employeeId, int? processId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var q = BuildQuery(db, start, end, employeeId, processId);

            // 按员工分组聚合（SQL 端完成求和，避免全表加载）
            var grouped = await q
                .GroupBy(w => new { w.EmployeeId, w.Employee!.Name })
                .Select(g => new SalarySummaryDto
                {
                    EmployeeId = g.Key.EmployeeId,
                    EmployeeName = g.Key.Name,
                    ReportCount = g.Sum(w => w.ReportCount),
                    Wage = g.Sum(w => w.Wage),
                    Times = g.Count()
                })
                .OrderBy(x => x.EmployeeId)
                .ToListAsync();

            // 补充分组信息：员工参与过的工组名称（去重）
            var employeeIds = grouped.Select(g => g.EmployeeId).ToList();
            var groupMap = await db.WorkReports.AsNoTracking()
                .Where(w => employeeIds.Contains(w.EmployeeId))
                .Select(w => new { w.EmployeeId, w.WorkGroup!.Name })
                .Distinct()
                .ToListAsync();

            var map = groupMap
                .GroupBy(x => x.EmployeeId)
                .ToDictionary(g => g.Key, g => string.Join("、", g.Select(x => x.Name)));
            foreach (var item in grouped)
                if (map.TryGetValue(item.EmployeeId, out var names))
                    item.WorkGroupNames = names;

            return grouped;
        }

        public async Task<List<PerformanceSummaryDto>> GetPerformanceSummaryAsync(DateTime? start, DateTime? end, string groupBy, int? filterId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var q = BuildQuery(db, start, end,
                groupBy == "Process" ? null : filterId,      // 按人员分组时 filterId 为员工 Id
                groupBy == "Employee" ? null : filterId);    // 按工序分组时 filterId 为工序 Id

            if (groupBy == "Process")
            {
                return await q
                    .GroupBy(w => new { w.ProcessId, w.Process!.Name })
                    .Select(g => new PerformanceSummaryDto
                    {
                        GroupId = g.Key.ProcessId,
                        GroupName = g.Key.Name,
                        ReportCount = g.Sum(w => w.ReportCount),
                        Wage = g.Sum(w => w.Wage),
                        Times = g.Count()
                    })
                    .OrderBy(x => x.GroupId)
                    .ToListAsync();
            }

            // 默认按人员分组
            return await q
                .GroupBy(w => new { w.EmployeeId, w.Employee!.Name })
                .Select(g => new PerformanceSummaryDto
                {
                    GroupId = g.Key.EmployeeId,
                    GroupName = g.Key.Name,
                    ReportCount = g.Sum(w => w.ReportCount),
                    Wage = g.Sum(w => w.Wage),
                    Times = g.Count()
                })
                .OrderBy(x => x.GroupId)
                .ToListAsync();
        }

        public async Task<ReportTotalDto> GetTotalAsync(DateTime? start, DateTime? end, int? employeeId, int? processId)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var q = BuildQuery(db, start, end, employeeId, processId);
            var total = await q.GroupBy(_ => 1)
                .Select(g => new ReportTotalDto
                {
                    ReportCount = g.Sum(w => w.ReportCount),
                    Wage = g.Sum(w => w.Wage),
                    Times = g.Count()
                })
                .FirstOrDefaultAsync();
            return total ?? new ReportTotalDto();
        }

        public async Task<List<OptionDto>> GetEmployeeOptionsAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Employees.AsNoTracking().OrderBy(e => e.Id)
                .Select(e => new OptionDto { Id = e.Id, Text = e.Name + "（" + e.EmployeeNo + "）" })
                .ToListAsync();
        }

        public async Task<List<OptionDto>> GetProcessOptionsAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Processes.AsNoTracking().OrderBy(p => p.Id)
                .Select(p => new OptionDto { Id = p.Id, Text = p.Name })
                .ToListAsync();
        }
    }
}
