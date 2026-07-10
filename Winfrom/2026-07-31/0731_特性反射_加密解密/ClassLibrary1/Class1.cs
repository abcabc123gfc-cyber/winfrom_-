using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1
    {
        public int Id { get; set; } = 100;
        public string Name { get; set; } = "吴亦凡";
        public void Test1()
        {
            Console.WriteLine("+++++++");
            Console.WriteLine("Test1");
        }
        public string Test2(string content)
        {
            return "Test2" + content;
        }
    }
}
