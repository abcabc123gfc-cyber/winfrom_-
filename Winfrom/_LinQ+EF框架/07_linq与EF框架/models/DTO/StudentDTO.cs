using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models.DTO
{
    //页面上要显示什么,这里面就写什么
    internal class StudentDTO
    {
        //这是一个新的模型,这个类不会映射到数据库的表中,这个是用来存储查询结果的(类似于数据库中的视图)
      
        public int Id { get; set; }

        public string StuName { get; set; }
        public int? Age { get; set; }
        public string Sex { get; set; }

        public string ClassRoomName { get; set; }
        public string CreateUserName { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastUpdateUserName { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string Status { get; set; }
    }
}
