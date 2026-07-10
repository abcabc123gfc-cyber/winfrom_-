using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Domains
{

    [Table("StudentAddress")]
    public class StudentAddress
    {
        public int StudentAddressId { get; set; }
        public string AddressName { get; set; }
    }
}
