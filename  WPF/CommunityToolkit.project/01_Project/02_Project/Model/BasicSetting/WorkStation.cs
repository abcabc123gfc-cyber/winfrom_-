using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _02_Project.Model.BasicSetting
{
    /// <summary>
    /// 工位（基础设置 - 工位管理）
    /// 网页字段：选择车间（必填）、选择生产线（必填）、工位名称（必填）、备注
    /// </summary>
    [Table("WorkStation")]
    public class WorkStation
    {
        [Key]
        public int Id { get; set; }

        /// <summary>选择车间（所属车间 Id）</summary>
        public int WorkshopId { get; set; }

        /// <summary>所属车间</summary>
        public Workshop? Workshop { get; set; }

        /// <summary>选择生产线（所属生产线 Id）</summary>
        public int ProductionLineId { get; set; }

        /// <summary>所属生产线</summary>
        public ProductionLine? ProductionLine { get; set; }

        /// <summary>工位名称</summary>
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>备注</summary>
        [MaxLength(200)]
        public string? Remark { get; set; }

        /// <summary>关联的工序（工序管理 - 子表单[工位]，多对多）</summary>
        public ICollection<ProcessWorkStation> ProcessWorkStations { get; set; } = new List<ProcessWorkStation>();
    }
}
