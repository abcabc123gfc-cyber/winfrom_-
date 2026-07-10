using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User : IUser
    {
        public string Name { get; set; }
        public string Residence { get; set; }
        public char Sex { get; set; }          // 修正为 Pascal 风格（可选）
        public DateTime Birth { get; set; }
        public string BirthAddress { get; set; }
        public string Political { get; set; }
        public DateTime Party { get; set; }
        public string Ethnicity { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Language { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; } // 修正首字母大写
        public string PictureFIlePath { get ; set ; }
    }
}
