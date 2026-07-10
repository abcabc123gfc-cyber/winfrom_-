using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_对象的创建和保存.Model { 

    //特性
    //默认情况下,对象不能被序列化  只有添加了这个特性之后 才能被序列化
   [Serializable]
    public class People
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
    }
}
