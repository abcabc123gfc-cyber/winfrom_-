using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace day05
{
    internal class MyDateTime
    {

        //DateTime类 用来进行时间操作
        //dateTime 类型变量 可以保存日期和时间
        public void TestDateTime()
        {
            // 创建一个 DateTime 对象，默认值为 0001-01-01 00:00:00（非当前时间）
            DateTime dt = new DateTime();

            // 获取当前本地日期和时间（含时区偏移）
            DateTime dt1 = DateTime.Now;

            // 获取当前本地日期（时间为 00:00:00）
            DateTime dt2 = DateTime.Today;

            // 获取当前协调世界时（UTC）日期和时间
            DateTime dt3 = DateTime.UtcNow;

            // 获取当前本地时间加上 1 天后的日期时间
            DateTime dt4 = DateTime.Now.AddDays(1);

            // 获取当前本地时间加上 1 个月后的日期时间
            DateTime dt5 = DateTime.Now.AddMonths(1);

            // 获取当前本地时间加上 1 年后的日期时间
            DateTime dt6 = DateTime.Now.AddYears(1);

            // 获取当前本地时间加上 1 小时后的日期时间
            DateTime dt7 = DateTime.Now.AddHours(1);

            Console.WriteLine("今天的星期是:"+dt.DayOfWeek);

            DateTime dateTime= TimeSpan.FromDays(1);
            #region 常用的时间格式
            /*
                yy 表示年份的后两位数字
                yyyy 4位年份
                M 月份 月份加日期
                MM 单月份 小数值
                MMM 小写月份加 月 字
                MMMM 大写月份全称
                dd 日
                d 日
                HH 24小时制
                h 12小时制
                mm 分钟
                ss 秒
                tt AM/PM
             */
            #endregion

            

            Console.WriteLine( dt.ToString("mm")); //00
            Console.WriteLine( dt.ToString("m")); //Z1月1日
            // 可根据需要输出值进行验证
            Console.WriteLine($"默认: {dt}");
            Console.WriteLine($"Now: {dt1}");
            Console.WriteLine($"Today: {dt2}");
            Console.WriteLine($"UtcNow: {dt3}");
            Console.WriteLine($"+1天: {dt4}");
            Console.WriteLine($"+1月: {dt5}");
            Console.WriteLine($"+1年: {dt6}");
            Console.WriteLine($"+1小时: {dt7}");

            //时间戳
            //总秒数
            Console.WriteLine(new DateTimeOffset(dt1).ToUnixTimeSeconds());
            //总毫秒数
            Console.WriteLine(new DateTimeOffset(dt1).ToUnixTimeMilliseconds());
        }
    }
}
