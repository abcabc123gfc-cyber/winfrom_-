using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_XML序列化.Model
{

    [Serializable]
    public class Student
    {
        public string StuName { get; set; }
        public int StuAge { get; set; }
        public string StuGender { get; set; }
        public string StuClass { get; set; }
    }
}
