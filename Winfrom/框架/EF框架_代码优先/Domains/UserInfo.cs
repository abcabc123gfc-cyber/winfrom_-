using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF框架_代码优先.Domains
{
    //指定表名
    [Table("UserInfo")]
    internal class UserInfo
    {
        //指定主键
        [Key]
        //指定自增
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]
        [MinLength(1)]
        [Column(TypeName ="varchar")]
        //不能为空,相当于 not null
        [Required(ErrorMessage = "用户名不能为空")]
        public string Account { get; set; }

        [MaxLength(20)]
        [MinLength(1)]
        [Column(TypeName = "varchar")]
        //不能为空,相当于 not null
        [Required(ErrorMessage = "用户名不能为空")]
        public string Password { get; set; }

        //指定范围 0-1
        [Range(0, 1)]
        [Required(ErrorMessage = "用户名不能为空")]
        [Column(TypeName = "int")]
        public int State { get; set; }

        //忽略此字段, 不映射为数据库字段
        [NotMapped]
        public string UserName { get; set; }
    }
}
