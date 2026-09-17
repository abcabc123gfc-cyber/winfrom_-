using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 接口_依赖注入
{
    internal class 接口隔离_胖接口
    {
        static void Main(string[] args)
        {
            int[] nums1 = { 1, 2, 3, 4 };
            ArrayList nums2=new ArrayList { 1, 2, 3, 4 };
            Console.WriteLine(Sum(nums1));
            Console.WriteLine(Sum(nums2));
        }

        private static int Sum(ICollection nums1)
        {
            return default;   
        }
    }
    class ReadOnlyCollection : IEnumerable
    {
        /// <summary>
        /// 当迭代对象的时候_ 需要提供一个迭代器
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        //成员类: 原因: 声明在外部可能会 污染 名称空间
        public class Enumerator : IEnumerator
        {
            public object Current => throw new NotImplementedException();

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
