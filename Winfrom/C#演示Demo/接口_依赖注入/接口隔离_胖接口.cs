using System;
using System.Collections;

namespace 接口_依赖注入
{
    internal class 接口隔离_胖接口
    {
        //static void Main(string[] args)
        //{
        //    int[] nums1 = { 1, 2, 3, 4 };
        //    ArrayList nums2 = new ArrayList { 1, 2, 3, 4 };
        //    Console.WriteLine(Sum(nums1));
        //    Console.WriteLine(Sum(nums2));
        //    var roc= new ReadOnlyCollection(nums1);
        //    Console.WriteLine(Sum(roc));
        //    //只读的集合能够被 foreach 进行迭代
        //    //foreach 语句背后 通过Enumerator 的 Current元素 迭代一遍
        //    //foreach (var item in roc)
        //    //{
        //    //    Console.WriteLine(item);
        //    //}
        //    //此时Sum 无法接收 roc
        //    //原因: 传递的接口太胖: 现在需求是 迭代 ICollection 最喜爱迭代的基础上增加了 Count等属性 : 接口太胖
        //    //影响: 将一些合格的service provider 限制了
        //    //解决方法: 将 ICollection 替换为IEnumerable
        //    //原因: 在服务中使用者中 迭代对象时 只需要 迭代对象即可 ,无需其他属性方法
        //    //符合接口隔离原则: 调用者绝不多要
        //    //传递 进去的接口不应该 有用不到的功能
        //}

        private static int Sum(IEnumerable nums1)
        {
            int sum = 0;
            foreach (var item in nums1)
            {
                sum += (int)item;
            }
            return sum;
        }
    }
    class ReadOnlyCollection : IEnumerable
    {
        private int[] _array;
        public ReadOnlyCollection(int[] array)
        {
            //初始化之后 不能修改
            this._array = array;
        }
        /// <summary>
        /// 当迭代对象的时候_ 需要提供一个迭代器
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerator GetEnumerator()
        {
            // 迭代器 _当需要迭代的时候 返回 Enumerator 实例
            //被迭代器所迭代的对象
            return new Enumerator(this);
        }
        //成员类: 原因: 声明在外部可能会 污染 名称空间
        //实现了只能迭代的对象 不能修改 
        public class Enumerator : IEnumerator
        {
            private ReadOnlyCollection _collection;
            //数组中的元素索引所指向的元素
            private int _head;
            public Enumerator(ReadOnlyCollection readOnlyCollection)
            {
                this._collection = readOnlyCollection;
                _head = -1;
            }

            public object Current
            {
                get
                {
                    //作为成员类 能够访问外部类的 私有成员变量
                    object o = _collection._array[_head];
                    return o;
                }
            }

            public bool MoveNext()
            {
                if (++_head < _collection._array.Length)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
               _head= -1;
            }
        }
    }
}
