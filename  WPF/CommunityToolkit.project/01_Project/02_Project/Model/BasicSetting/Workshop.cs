using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _02_Project.Model.BasicSetting
{
    /// <summary>
    /// 车间（基础设置 - 车间管理）
    /// 网页字段：车间名称（必填）
    /// </summary>
    [Table("Workshop")]
    public class Workshop
    {
        [Key]
        public int Id { get; set; }

        /// <summary>车间名称</summary>
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>车间下的生产线（选择车间）</summary>
        public ICollection<ProductionLine> ProductionLines { get; set; } = new List<ProductionLine>();

        /// <summary>车间下的工位（选择车间）</summary>
        public ICollection<WorkStation> WorkStations { get; set; } = new List<WorkStation>();

        /// <summary>车间下的员工（选择车间）</summary>
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
