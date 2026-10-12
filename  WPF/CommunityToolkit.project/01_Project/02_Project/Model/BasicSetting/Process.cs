using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using _02_Project.Model.Report;
namespace _02_Project.Model.BasicSetting
{
    /// <summary>
    /// 工序（基础设置 - 工序管理）
    /// 网页字段：工序名称（必填）、备注、子表单[工位]（可绑定多个工位 → 多对多）
    /// </summary>
    [Table("Process")]
    public class Process
    {
        [Key]
        public int Id { get; set; }

        /// <summary>工序名称</summary>
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>备注</summary>
        [MaxLength(200)]
        public string? Remark { get; set; }

        /// <summary>关联工位（工序管理 - 子表单[工位]，多对多）</summary>
        public ICollection<ProcessWorkStation> ProcessWorkStations { get; set; } = new List<ProcessWorkStation>();

        /// <summary>包含该工序的工艺路线明细</summary>
        public ICollection<CraftRouteDetail> CraftRouteDetails { get; set; } = new List<CraftRouteDetail>();

        /// <summary>该工序的工价记录</summary>
        public ICollection<WageSetting> WageSettings { get; set; } = new List<WageSetting>();

        /// <summary>该工序的报工单</summary>
        public ICollection<WorkReport> WorkReports { get; set; } = new List<WorkReport>();
    }
}
