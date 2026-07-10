using System;
using System.Timers;

namespace day16
{
    internal class 事件
    {
        public void EventTset()
        {
            //属性表示 当前的类或对象处于什么状态
            //方法表示:它能做什么
            //事件: 表示他能在什么情况下通知谁
            //类或对象最重要的三个功能: 存储数据 做事情 通知别人

            //事件的拥有者: timer
            //事件: Elapsed
            //事件的响应者: boy
            //事件处理器: Action 方法
            //事件订阅:  timer.Elapsed += boy.Action;
            //事件订阅操作符: +=
            Timer timer = new Timer();
            //tmer 对象中的事件 标识是 闪电符号
            timer.Interval = 1000;
            Girl girl = new Girl();
           
            Boy boy = new Boy();
            timer.Elapsed += boy.Action;
            timer.Elapsed += girl.Action;
            timer.Start();
            Console.ReadKey();
        }
    }
    class Boy
    {
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Jump!");
        }
    }
    class Girl
    {
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Sing!");
        }
    }
}
