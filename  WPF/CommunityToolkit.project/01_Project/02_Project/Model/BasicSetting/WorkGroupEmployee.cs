using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _02_Project.Model.BasicSetting
{
    /// <summary>
    /// 工组 - 员工 多对多连接表
    /// </summary>
    [Table("WorkGroupEmployee")]
    [PrimaryKey(nameof(WorkGroupId), nameof(EmployeeId))]
    public class WorkGroupEmployee
    {
        public int WorkGroupId { get; set; }

        public WorkGroup? WorkGroup { get; set; }

        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}
