using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLight.Model
{
    [Table("Blog")]
   
    public class BlogInfo
    {
        [Column("BlogId")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogId { get; set;}
        public string BlogTitle { get; set;}
        public string BlogContent { get; set;}
        public int CreateUserId { get; set;}
        
        public DateTime CreateTime { get; set;}
    }
}
