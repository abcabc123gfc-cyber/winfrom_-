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
    /// 学生
    /// </summary>
    [Table("Students")]
    public class Student
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentsId { get; set; }
        public string StudentName { get; set; }

        //外键
        public int StudentAddressId { get; set; }


        //学生所学的课程  一个学生可以学多门课程,所以需要是一个集合
        [Required]
        public  virtual ICollection<Course> Courses { get; set; }

        //实体导航属性  获取一个学生的时候,想知道学生的地址信息,就可以通过这个属性来获取
        public virtual StudentAddress StudentAddress { get; set; }






    }
}
