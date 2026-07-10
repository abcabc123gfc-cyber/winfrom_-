using System;

namespace day18
{
    internal class lamdba_表达式
    {
        public static void Test()
        {
            // lambda 表达式
            //格式: (参数列表) => {方法体}
            Func<int, int> func = (x) => x * x;
            Func<int, int, int> func1 = (y, x) =>
            {
                return x * y;
            };
            Practice.Test(12, 15);
        }

    }
}
