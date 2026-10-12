using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _02_Project.Service
{
    /// <summary>下拉选项（ComboBox 通用：Id + Text）</summary>
    public class OptionDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }

    /// <summary>车间（Workshop 表展示模型）</summary>
    public class WorkshopDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>生产线（ProductionLine 表展示模型）</summary>
    public class ProductionLineDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int WorkshopId { get; set; }
        public string WorkshopName { get; set; } = string.Empty;
    }

    /// <summary>工位（WorkStation 表展示模型）</summary>
    public class WorkStationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int WorkshopId { get; set; }
        public string WorkshopName { get; set; } = string.Empty;
        public int ProductionLineId { get; set; }
        public string ProductionLineName { get; set; } = string.Empty;
        public string? Remark { get; set; }
    }

    /// <summary>员工（Employee 表展示模型）</summary>
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmployeeNo { get; set; } = string.Empty;
        public int WorkshopId { get; set; }
        public string WorkshopName { get; set; } = string.Empty;
    }

    /// <summary>工组（WorkGroup 表展示模型，含成员统计）</summary>
    public class WorkGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MemberCount { get; set; }
        public string MembersText { get; set; } = string.Empty;
    }

    /// <summary>工序（Process 表展示模型，含绑定工位统计）</summary>
    public class ProcessDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Remark { get; set; }
        public int StationCount { get; set; }
        public string StationsText { get; set; } = string.Empty;
    }

    /// <summary>
    /// 基础设置服务接口（对应“数据库操作与设计逻辑记录.md”中的基础设置模块）：
    /// 车间 / 生产线 / 工位 / 员工 / 工组 / 工序 六类基础数据的增删改查。
    /// 删除保护：基础数据被业务数据（报工单/工价/工艺路线）引用时禁止物理删除。
    /// </summary>
    public interface IBasicSettingService
    {
        #region 车间 Workshop

        Task<List<WorkshopDto>> GetWorkshopsAsync(int? id, int pageIndex, int pageSize);
        Task<int> GetWorkshopCountAsync(int? id);
        /// <summary>全部车间（下拉框使用）</summary>
        Task<List<OptionDto>> GetWorkshopOptionsAsync();
        Task<AuthResult> AddWorkshopAsync(string name, int operatorUserId);
        Task<AuthResult> UpdateWorkshopAsync(int id, string name, int operatorUserId);
        /// <summary>删除车间：先检查其下是否有生产线/工位，有则禁止删除</summary>
        Task<AuthResult> DeleteWorkshopAsync(int id);

        #endregion

        #region 生产线 ProductionLine

        Task<List<ProductionLineDto>> GetProductionLinesAsync(int? id, int? workshopId, int pageIndex, int pageSize);
        Task<int> GetProductionLineCountAsync(int? id, int? workshopId);
        /// <summary>某车间下的生产线（下拉框使用，workshopId 为 null 时返回全部）</summary>
        Task<List<OptionDto>> GetProductionLineOptionsAsync(int? workshopId);
        Task<AuthResult> AddProductionLineAsync(string name, int workshopId, int operatorUserId);
        Task<AuthResult> UpdateProductionLineAsync(int id, string name, int workshopId, int operatorUserId);
        /// <summary>删除生产线：先检查其下是否有工位，有则禁止删除</summary>
        Task<AuthResult> DeleteProductionLineAsync(int id);

        #endregion

        #region 工位 WorkStation

        Task<List<WorkStationDto>> GetWorkStationsAsync(int? id, int? workshopId, int? productionLineId, int pageIndex, int pageSize);
        Task<int> GetWorkStationCountAsync(int? id, int? workshopId, int? productionLineId);
        /// <summary>全部工位（工序管理子表单使用）</summary>
        Task<List<OptionDto>> GetWorkStationOptionsAsync();
        Task<AuthResult> AddWorkStationAsync(string name, int workshopId, int productionLineId, string? remark, int operatorUserId);
        Task<AuthResult> UpdateWorkStationAsync(int id, string name, int workshopId, int productionLineId, string? remark, int operatorUserId);
        /// <summary>删除工位：先检查工序是否绑定该工位，有则禁止删除</summary>
        Task<AuthResult> DeleteWorkStationAsync(int id);

        #endregion

        #region 员工 Employee

        Task<List<EmployeeDto>> GetEmployeesAsync(int? id, int? workshopId, int pageIndex, int pageSize);
        Task<int> GetEmployeeCountAsync(int? id, int? workshopId);
        /// <summary>某车间下的员工（下拉框使用，workshopId 为 null 时返回全部）</summary>
        Task<List<OptionDto>> GetEmployeeOptionsAsync(int? workshopId);
        Task<AuthResult> AddEmployeeAsync(string name, string employeeNo, int workshopId, int operatorUserId);
        Task<AuthResult> UpdateEmployeeAsync(int id, string name, string employeeNo, int workshopId, int operatorUserId);
        /// <summary>删除员工：存在报工记录或仍属于工组时禁止删除</summary>
        Task<AuthResult> DeleteEmployeeAsync(int id);

        #endregion

        #region 工组 WorkGroup（含 工组-员工 多对多子表单）

        Task<List<WorkGroupDto>> GetWorkGroupsAsync(int? id, int pageIndex, int pageSize);
        Task<int> GetWorkGroupCountAsync(int? id);
        /// <summary>全部工组（下拉框使用）</summary>
        Task<List<OptionDto>> GetWorkGroupOptionsAsync();
        Task<AuthResult> AddWorkGroupAsync(string name, List<int> employeeIds, int operatorUserId);
        Task<AuthResult> UpdateWorkGroupAsync(int id, string name, List<int> employeeIds, int operatorUserId);
        /// <summary>删除工组：存在报工单引用时禁止删除</summary>
        Task<AuthResult> DeleteWorkGroupAsync(int id);
        /// <summary>某工组已绑定的员工（编辑回显子表单）</summary>
        Task<List<OptionDto>> GetWorkGroupEmployeeOptionsAsync(int workGroupId);

        #endregion

        #region 工序 Process（含 工序-工位 多对多子表单）

        Task<List<ProcessDto>> GetProcessesAsync(int? id, int pageIndex, int pageSize);
        Task<int> GetProcessCountAsync(int? id);
        /// <summary>全部工序（下拉框使用）</summary>
        Task<List<OptionDto>> GetProcessOptionsAsync();
        Task<AuthResult> AddProcessAsync(string name, string? remark, List<int> stationIds, int operatorUserId);
        Task<AuthResult> UpdateProcessAsync(int id, string name, string? remark, List<int> stationIds, int operatorUserId);
        /// <summary>删除工序：被工价/工艺路线/报工单引用时禁止删除</summary>
        Task<AuthResult> DeleteProcessAsync(int id);
        /// <summary>某工序已绑定的工位（编辑回显子表单）</summary>
        Task<List<OptionDto>> GetProcessStationOptionsAsync(int processId);

        #endregion
    }
}
