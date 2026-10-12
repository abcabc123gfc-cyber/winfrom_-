using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _02_Project.Service
{
    /// <summary>薪资统计行（按员工分组：累加数量与工资）</summary>
    public class SalarySummaryDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        /// <summary>该员工参与过的工组（去重后拼接展示）</summary>
        public string WorkGroupNames { get; set; } = string.Empty;
        public int ReportCount { get; set; }
        public decimal Wage { get; set; }
        public int Times { get; set; }
    }

    /// <summary>业绩统计行（按人员或按工序分组）</summary>
    public class PerformanceSummaryDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public int ReportCount { get; set; }
        public decimal Wage { get; set; }
        public int Times { get; set; }
    }

    /// <summary>薪资报表合计</summary>
    public class ReportTotalDto
    {
        public int ReportCount { get; set; }
        public decimal Wage { get; set; }
        public int Times { get; set; }
    }

    /// <summary>
    /// 薪资报表服务接口（对应“数据库操作与设计逻辑记录.md”第三章查询与统计）：
    /// 薪资统计：按 WorkReport.EmployeeId 分组，累加 Wage；
    /// 业绩统计：按 StartTime/EndTime 时间段过滤，按人/工序分组统计。
    /// </summary>
    public interface ISalaryReportService
    {
        /// <summary>薪资统计：按员工分组（可按时间段/员工/工序过滤）</summary>
        Task<List<SalarySummaryDto>> GetSalarySummaryAsync(DateTime? start, DateTime? end, int? employeeId, int? processId);

        /// <summary>业绩统计：groupBy = "Employee" 按人员 / "Process" 按工序（filterId 对应当前分组的实体 Id）</summary>
        Task<List<PerformanceSummaryDto>> GetPerformanceSummaryAsync(DateTime? start, DateTime? end, string groupBy, int? filterId);

        /// <summary>当前筛选条件下的合计（数量/工资/报工次数）</summary>
        Task<ReportTotalDto> GetTotalAsync(DateTime? start, DateTime? end, int? employeeId, int? processId);

        /// <summary>全部员工（报表筛选下拉）</summary>
        Task<List<OptionDto>> GetEmployeeOptionsAsync();
        /// <summary>全部工序（报表筛选下拉）</summary>
        Task<List<OptionDto>> GetProcessOptionsAsync();
    }
}
