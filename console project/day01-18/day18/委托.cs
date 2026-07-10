using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace day18
{
    internal class 委托
    {
        // Test
        public void Demo()
        {
            //Delegate T Fn(T a)
            Action<int, int> action = (x, y) => Console.WriteLine(x + y);
        }
    }
}
