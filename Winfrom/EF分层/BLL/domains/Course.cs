using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Domains
{
    /// <summary>
    /// 课程
    /// </summary>
    [Table("Courses")]
    public class Course
    {
        public int CourseId { get; set; }
        public string CouredName { get; set; }


        //获取一个课程,想知道这个课程有那些学生在学

        [Required]
        public virtual ICollection<Student> Students { get; set; }
    }
}
