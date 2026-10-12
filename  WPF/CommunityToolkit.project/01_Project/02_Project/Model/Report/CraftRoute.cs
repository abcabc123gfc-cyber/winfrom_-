using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _02_Project.Model.Report
{
    /// <summary>
    /// 工艺路线（报工管理 - 工艺路线管理）
    /// 网页字段：工艺路线编号、工艺名称（必填）、成名名称（必填，模板原始字段名，疑为"成品名称"）、工艺流程（子表单：工序）、备注
    /// </summary>
    [Table("CraftRoute")]
    public class CraftRoute
    {
        [Key]
        public int Id { get; set; }

        /// <summary>工艺路线编号</summary>
        [Required, MaxLength(20)]
        public string RouteNo { get; set; } = string.Empty;

        /// <summary>工艺名称</summary>
        [Required, MaxLength(50)]
        public string RouteName { get; set; } = string.Empty;

        /// <summary>成名名称（模板原始字段名，应为成品名称）</summary>
        [Required, MaxLength(50)]
        public string ProductName { get; set; } = string.Empty;

        /// <summary>备注</summary>
        [MaxLength(200)]
        public string? Remark { get; set; }

        /// <summary>工艺流程明细（子表单：选择工序 + 顺序 + 备注）</summary>
        public ICollection<CraftRouteDetail> Details { get; set; } = new List<CraftRouteDetail>();
    }
}
