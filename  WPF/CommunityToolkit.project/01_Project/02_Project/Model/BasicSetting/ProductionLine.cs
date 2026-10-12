using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _02_Project.Model.BasicSetting
{
    /// <summary>
    /// 生产线（基础设置 - 生产线管理）
    /// 网页字段：生产线名称（必填）、选择车间（必填）
    /// </summary>
    [Table("ProductionLine")]
    public class ProductionLine
    {
        [Key]
        public int Id { get; set; }

        /// <summary>生产线名称</summary>
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>选择车间（所属车间 Id）</summary>
        public int WorkshopId { get; set; }

        /// <summary>所属车间</summary>
        public Workshop? Workshop { get; set; }

        /// <summary>该生产线下的工位（选择生产线）</summary>
        public ICollection<WorkStation> WorkStations { get; set; } = new List<WorkStation>();
    }
}
