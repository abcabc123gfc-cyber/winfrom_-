using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1.Models
{

    [Table("UserInfo")]
    internal class UserInfo
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,Column(TypeName ="varchar"),MaxLength(50)]
        public string Account {  get; set; }

        [Required,Column(TypeName ="varchar"),MaxLength(50)]
        public string Password  {  get; set; }

        //DefaultValue 设置默认值
        [Required,DefaultValue(0)]
        public int? Status { get; set; }


        [Required]
        public DateTime Create { get; set; } = DateTime.Now;

        //[Required]
        //没有添加Required特性之前   标识属性可以为null
       //但是int类型 是值类型 不能存储 null
       //数据类型? 表示可空类型  数据类型在原本可以存储的基础上 增加了null
        public int LastUpdateUserId { get; set; }
        public DateTime? LastUpdateTime { get; set; }


    }
}
