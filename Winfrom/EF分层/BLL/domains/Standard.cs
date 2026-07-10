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
    /// 评级
    /// </summary>
    [Table("Standards")]
    public class Standard
    {
        public int StandardId { get; set; }
        public string StandardName { get; set; }
        //拿到一个教师的评级的时候,想知道这个评级对应的教师有哪些
        //1:N  一对多,导航属性是一个集合
        [Required]
        public virtual ICollection<Teacher> Teachers{ get;set; }
    }
}
