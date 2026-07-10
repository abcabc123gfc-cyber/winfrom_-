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
    /// 教师
    /// </summary>
    [Table("Teachers")]
    public class Teacher
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }


        public string TeacherName { get; set; }

        //外键 老师的评级的id
        public int StandardId { get; set; }

        //外键 老师所教授的课程id
        public int CourseId { get; set; }



        //导航属性一般是虚拟的,并且在映射数据库的时候,不会生成具体的列
        //一个老师只有一个评级,所以此处导航属性是一个对象
        //如果一个老师有多个评价,那么次数导航属性就需要是一个集合
        //获取一个教师,想要获取教师的评级信息

        public virtual Standard Standard { get; set; }


        //获取一个教师的时候,想要获取教师教授的课程


        public virtual Course Course { get; set; }





    }
}
