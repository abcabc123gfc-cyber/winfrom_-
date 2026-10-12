using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using _02_Project.Model.BasicSetting;
namespace _02_Project.Model.Report
{
    /// <summary>
    /// 报工单 - 员工 多对多连接表
    /// </summary>
    [Table("WorkReportEmployee")]
    [PrimaryKey(nameof(WorkReportId), nameof(EmployeeId))]
    public class WorkReportEmployee
    {
        public int WorkReportId { get; set; }

        public WorkReport? WorkReport { get; set; }

        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}
