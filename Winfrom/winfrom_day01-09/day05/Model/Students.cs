using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _06_XML序列化.Model
{
   
    public class Students
    {
        [XmlElement("Student")] //特性  指定标签名
        public List<Student> StudentList { get; set; }
       
    }
}
