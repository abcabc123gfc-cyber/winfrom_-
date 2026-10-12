using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _02_Project.Service
{
    /// <summary>工价（WageSetting 表展示模型）</summary>
    public class WageSettingDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ProcessId { get; set; }
        public string ProcessName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
    }

    /// <summary>工艺路线（CraftRoute 表展示模型）</summary>
    public class CraftRouteDto
    {
        public int Id { get; set; }
        public string RouteNo { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string? Remark { get; set; }
        public int DetailCount { get; set; }
    }

    /// <summary>工艺路线明细行（工艺流程子表单：工序 + 顺序 + 备注）</summary>
    public class CraftRouteDetailDto
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public string? Remark { get; set; }
    }

    /// <summary>报工单（WorkReport 表展示模型）</summary>
    public class WorkReportDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public int WorkGroupId { get; set; }
        public string WorkGroupName { get; set; } = string.Empty;
        public int ProcessId { get; set; }
        public string ProcessName { get; set; } = string.Empty;
        public int WageSettingId { get; set; }
        public string WageSettingName { get; set; } = string.Empty;
        public int ReportCount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Wage { get; set; }
        public int EmployeeCount { get; set; }
        public string EmployeeNames { get; set; } = string.Empty;
        public string? Remark { get; set; }
    }

    /// <summary>报工单保存输入模型（主表 + 子表单员工）</summary>
    public class WorkReportInput
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EmployeeId { get; set; }
        public int WorkGroupId { get; set; }
        public int ProcessId { get; set; }
        public int WageSettingId { get; set; }
        public int ReportCount { get; set; }
        public string? Remark { get; set; }
        /// <summary>子表单[员工姓名]选中的员工 Id 列表</summary>
        public List<int> EmployeeIds { get; set; } = new();
    }

    /// <summary>
    /// 报工管理服务接口（对应“数据库操作与设计逻辑记录.md”中的工艺路线 + 报工管理模块）：
    /// 工价 WageSetting / 工艺路线 CraftRoute(+明细) / 报工单 WorkReport(+子表单员工)。
    /// 报工单业务规则：选工价自动带单价、工资=数量×单价、员工个数=子表单行数；历史数据删除保护。
    /// </summary>
    public interface IReportSettingService
    {
        #region 工价 WageSetting

        Task<List<WageSettingDto>> GetWageSettingsAsync(int? id, int? processId, int pageIndex, int pageSize);
        Task<int> GetWageSettingCountAsync(int? id, int? processId);
        /// <summary>工价下拉（processId 为 null 时返回全部，Text 带单价便于选择）</summary>
        Task<List<OptionDto>> GetWageSettingOptionsAsync(int? processId);
        Task<AuthResult> AddWageSettingAsync(string name, int processId, decimal unitPrice, string? description, int operatorUserId);
        Task<AuthResult> UpdateWageSettingAsync(int id, string name, int processId, decimal unitPrice, string? description, int operatorUserId);
        /// <summary>删除工价：被报工单引用时禁止删除</summary>
        Task<AuthResult> DeleteWageSettingAsync(int id);

        #endregion

        #region 工艺路线 CraftRoute（含工艺流程子表单）

        Task<List<CraftRouteDto>> GetCraftRoutesAsync(int? id, int pageIndex, int pageSize);
        Task<int> GetCraftRouteCountAsync(int? id);
        /// <summary>全部工序（工艺流程子表单下拉）</summary>
        Task<List<OptionDto>> GetProcessOptionsAsync();
        Task<AuthResult> AddCraftRouteAsync(string routeNo, string routeName, string productName, string? remark, List<CraftRouteDetailDto> details, int operatorUserId);
        Task<AuthResult> UpdateCraftRouteAsync(int id, string routeNo, string routeName, string productName, string? remark, List<CraftRouteDetailDto> details, int operatorUserId);
        /// <summary>删除工艺路线（明细随主表级联删除）</summary>
        Task<AuthResult> DeleteCraftRouteAsync(int id);
        /// <summary>某工艺路线的明细（编辑回显子表单，按顺序排序）</summary>
        Task<List<CraftRouteDetailDto>> GetCraftRouteDetailsAsync(int craftRouteId);

        #endregion

        #region 报工单 WorkReport（含子表单员工）

        Task<List<WorkReportDto>> GetWorkReportsAsync(int? id, int? employeeId, DateTime? start, DateTime? end, int pageIndex, int pageSize);
        Task<int> GetWorkReportCountAsync(int? id, int? employeeId, DateTime? start, DateTime? end);
        /// <summary>全部员工/某工组内员工（报工单子表单多选用）</summary>
        Task<List<OptionDto>> GetEmployeeOptionsAsync(int? workGroupId);
        /// <summary>全部工组（下拉）</summary>
        Task<List<OptionDto>> GetWorkGroupOptionsAsync();
        /// <summary>报工单已选的子表单员工（编辑回显）</summary>
        Task<List<OptionDto>> GetWorkReportEmployeeOptionsAsync(int workReportId);
        Task<AuthResult> AddWorkReportAsync(WorkReportInput input, int operatorUserId);
        Task<AuthResult> UpdateWorkReportAsync(int id, WorkReportInput input, int operatorUserId);
        /// <summary>删除报工单（子表单员工随主表级联删除）</summary>
        Task<AuthResult> DeleteWorkReportAsync(int id);

        #endregion
    }
}
