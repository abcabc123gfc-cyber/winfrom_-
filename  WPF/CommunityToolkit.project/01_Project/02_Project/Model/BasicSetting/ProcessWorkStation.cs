using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace _02_Project.Model.BasicSetting
{
    /// <summary>
    /// 工序 - 工位 多对多连接表（工序管理 - 子表单[工位]）
    /// </summary>
    [Table("ProcessWorkStation")]
    [PrimaryKey(nameof(ProcessId), nameof(WorkStationId))]
    public class ProcessWorkStation
    {
        public int ProcessId { get; set; }

        public Process? Process { get; set; }

        public int WorkStationId { get; set; }

        public WorkStation? WorkStation { get; set; }
    }
}
