using System;//
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day05
{
    internal class 时间的计算与对比
    {
        //时间的操作
        DateTime time = DateTime.Now;
        //可以通过调用DateTime的 方法修改当前时间对象
        public void Date()
        {
            //时间的加减
            time.AddDays(1);
            time.AddMonths(-1);
            time.AddYears(1);
            Console.WriteLine(time.Day);

            //比较时间 
            if(DateTime.Now  > new DateTime(2020, 1, 1))
            {
                Console.WriteLine("今天是2020年1月1日之后的时间");
            }

            // 直接使用 - 运算符 进行计算, 得到一个timeSpan 类型, 表示一个时间的间间隔
            TimeSpan ts = DateTime.Now - new DateTime(2020, 1, 1);
            Console.WriteLine("相差的天数"+ts.Days);
            Console.WriteLine("相差的秒"+ts.TotalSeconds);
           Console.WriteLine("相差的毫秒"+ts.TotalMilliseconds);

            
            // 方法一：转换为 UTC 时间（格林威治标准时间）
            //DateTime utcTime = DateTimeOffset.FromUnixTimeSeconds(ts.TotalSecond).UtcDateTime;

            // 方法二：直接转换为你的本地电脑时区时间（东八区）
            //DateTime localTime = DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;

            //Console.WriteLine("UTC时间：" + utcTime);      // 输出：2023/11/14 22:13:20
            //Console.WriteLine("本地时间：" + localTime);   // 输出：2023/11/15 6:13:20（北京时间）
        }
    }
}
