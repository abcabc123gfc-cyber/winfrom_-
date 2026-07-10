using System;

namespace day18
{
    internal class 泛型委托
    {
        public void Test()
        {

        }
        public delegate bool fn<T>(T value);

        /// <summary>
        /// 内置泛型委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Fn1<T>(T[] arr, Func<T, bool> predicate)
        {

            return default;
        }
        /// <summary>
        /// 泛型委托_自定义
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Fn2<T>(T[] arr, fn<T> value)
        {
            foreach (var item in arr)
            {
                if (value(item))
                {
                    return item.ToString();
                }

            }

            return default;
        }
    }
}
