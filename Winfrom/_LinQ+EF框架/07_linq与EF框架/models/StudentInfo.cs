using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    [Table("StudentInfo")]
    internal class StudentInfo
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }

        [Required,Column(TypeName ="varchar"),MaxLength(50)]
        public string StuName { get; set; }
        public int? Age {  get; set; }
        public bool? Sex { get; set;  }

        //班级id
        public int ClassId { get; set; }

        //创建人的id
        public int CreateUserId { get; set; }

        [Required]
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public int? LastUpdateUserId { get; set; }

        public DateTime LastUpdateTime { get; set; }


    }
}
