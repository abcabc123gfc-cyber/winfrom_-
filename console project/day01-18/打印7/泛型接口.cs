using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 打印7
{
    internal class 泛型接口
    {
    }
    interface ICalc<T>
    {
        T Add(T a, T b);
        T Sub(T a, T b);


    }
    class Calc : ICalc<int>
    {
        public int Add(int a, int b)
        {
           return a + b;
        }

        public int Sub(int a, int b)
        {
            return a - b;
        }
    }
    class Calc2<T> : ICalc<T>
    {
        public T Add(T a, T b)
        {
          return (T)(object)((int)(object)a + (int)(object)b);
        }

        public T Sub(T a, T b)
        {
            throw new NotImplementedException();
        }
    }
}
