using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_绑定值.ViewModel
{
    public class Window1ViewModel
    {
        public int id { get; set; } = 100;

        public string Name { get; set; } = "wuyifan";
        public List<string> ListData { get; set; }= new List<string>() { "吴亦凡","李云迪","蔡徐坤"};
    }
}
