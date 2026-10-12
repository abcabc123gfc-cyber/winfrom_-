using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using _02_Project.Model.Report;
namespace _02_Project.Model.BasicSetting
{
    /// <summary>
    /// 员工（基础设置 - 员工管理）
    /// </summary>
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        /// <summary>姓名</summary>
        [Required, MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        /// <summary>工号</summary>
        [Required, MaxLength(20)]
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>所属车间 Id</summary>
        public int WorkshopId { get; set; }

        /// <summary>所属车间</summary>
        public Workshop? Workshop { get; set; }

        /// <summary>所属工组（多对多）</summary>
        public ICollection<WorkGroupEmployee> WorkGroupEmployees { get; set; } = new List<WorkGroupEmployee>();

        /// <summary>参与过的报工记录（多对多）</summary>
        public ICollection<WorkReportEmployee> WorkReportEmployees { get; set; } = new List<WorkReportEmployee>();
    }
}
