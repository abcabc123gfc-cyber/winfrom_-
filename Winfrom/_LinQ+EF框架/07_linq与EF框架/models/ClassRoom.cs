using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    [Table("ClassRooms")]
    internal class ClassRoom
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "varchar"), MaxLength(50)]
        public string ClassRoomName { get; set; }

        public int? CreateUserId {  get; set; }

        [Required]
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public int? LastUpdateUserId { get; set; }

        public DateTime LastUpdateTime { get; set; }



    }
}
