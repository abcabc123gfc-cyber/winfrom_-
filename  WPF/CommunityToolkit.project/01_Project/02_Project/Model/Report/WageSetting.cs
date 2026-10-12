using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using _02_Project.Model.BasicSetting;
namespace _02_Project.Model.Report
{
    /// <summary>
    /// 工价（报工管理 - 工价管理）
    /// 网页字段：工价名称（必填）、工序（必填）、单价（必填）、工价说明
    /// </summary>
    [Table("WageSetting")]
    public class WageSetting
    {
        [Key]
        public int Id { get; set; }

        /// <summary>工价名称</summary>
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>选择的工序 Id</summary>
        public int ProcessId { get; set; }

        /// <summary>选择的工序</summary>
        public Process? Process { get; set; }

        /// <summary>单价（元/件）</summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        /// <summary>工价说明</summary>
        [MaxLength(200)]
        public string? Description { get; set; }

        /// <summary>按该工价结算的报工单</summary>
        public ICollection<WorkReport> WorkReports { get; set; } = new List<WorkReport>();
    }
}
