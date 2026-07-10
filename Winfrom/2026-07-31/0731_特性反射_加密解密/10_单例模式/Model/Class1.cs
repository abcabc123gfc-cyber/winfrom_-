using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_单例模式.Model
{
    internal class Class1
    {
        public string Attach()
        {
            Boss b=   Boss.GetBoss();
            return b.Sub().ToString();
        }
    }
}
