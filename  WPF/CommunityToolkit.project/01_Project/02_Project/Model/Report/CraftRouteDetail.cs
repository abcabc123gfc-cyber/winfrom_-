using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using _02_Project.Model.BasicSetting;
namespace _02_Project.Model.Report
{
    /// <summary>
    /// 工艺路线明细（工艺流程：选择工序、备注）
    /// </summary>
    [Table("CraftRouteDetail")]
    public class CraftRouteDetail
    {
        [Key]
        public int Id { get; set; }

        /// <summary>所属工艺路线 Id</summary>
        public int CraftRouteId { get; set; }

        /// <summary>所属工艺路线</summary>
        public CraftRoute? CraftRoute { get; set; }

        /// <summary>选择的工序 Id</summary>
        public int ProcessId { get; set; }

        /// <summary>选择的工序</summary>
        public Process? Process { get; set; }

        /// <summary>工艺顺序（按从小到大排序执行）</summary>
        public int SortOrder { get; set; }

        /// <summary>备注</summary>
        [MaxLength(200)]
        public string? Remark { get; set; }
    }
}
