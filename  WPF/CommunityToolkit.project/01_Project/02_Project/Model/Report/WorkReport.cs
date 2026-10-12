using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using _02_Project.Model.BasicSetting;
namespace _02_Project.Model.Report
{
    /// <summary>
    /// 报工管理（报工单）
    /// 网页字段：员工（必填）、工序（必填）、工价（必填）、报工数量（必填）、工资、备注、工组（必填）、员工个数、单价、子表单[员工姓名]
    /// </summary>
    [Table("WorkReport")]
    public class WorkReport
    {
        [Key]
        public int Id { get; set; }

        /// <summary>报工时间 - 开始（业绩统计报表：报工时间开始）</summary>
        public DateTime StartTime { get; set; }

        /// <summary>报工时间 - 结束（业绩统计报表：报工时间结束）</summary>
        public DateTime EndTime { get; set; }

        /// <summary>员工 Id（员工，必填）</summary>
        public int EmployeeId { get; set; }

        /// <summary>员工</summary>
        public Employee? Employee { get; set; }

        /// <summary>工组 Id（工组，必填）</summary>
        public int WorkGroupId { get; set; }

        /// <summary>工组</summary>
        public WorkGroup? WorkGroup { get; set; }

        /// <summary>工序 Id（工序，必填）</summary>
        public int ProcessId { get; set; }

        /// <summary>工序</summary>
        public Process? Process { get; set; }

        /// <summary>工价 Id（工价，必填）</summary>
        public int WageSettingId { get; set; }

        /// <summary>工价</summary>
        public WageSetting? WageSetting { get; set; }

        /// <summary>报工数量（必填）</summary>
        public int ReportCount { get; set; }

        /// <summary>单价（取自工价，可编辑）</summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        /// <summary>工资（报工数量 × 单价）</summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Wage { get; set; }

        /// <summary>员工个数（子表单记录数）</summary>
        public int EmployeeCount { get; set; }

        /// <summary>备注</summary>
        [MaxLength(200)]
        public string? Remark { get; set; }

        /// <summary>子表单[员工姓名]（报工单 - 员工 多对多明细）</summary>
        public ICollection<WorkReportEmployee> Employees { get; set; } = new List<WorkReportEmployee>();
    }
}
