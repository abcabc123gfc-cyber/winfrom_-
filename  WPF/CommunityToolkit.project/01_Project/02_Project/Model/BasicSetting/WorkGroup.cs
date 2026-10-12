using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using _02_Project.Model.Report;
namespace _02_Project.Model.BasicSetting
{
    /// <summary>
    /// 工组（基础设置 - 工组管理）
    /// 网页字段：工组名称（必填）、员工数量（子表单行数）、子表单[员工姓名]
    /// </summary>
    [Table("WorkGroup")]
    public class WorkGroup
    {
        [Key]
        public int Id { get; set; }

        /// <summary>工组名称</summary>
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>组内成员（多对多：工组 - 员工，即子表单[员工姓名]）</summary>
        public ICollection<WorkGroupEmployee> Members { get; set; } = new List<WorkGroupEmployee>();

        /// <summary>该工组的报工单（报工单 - 工组，单向外键）</summary>
        public ICollection<WorkReport> WorkReports { get; set; } = new List<WorkReport>();
    }
}
