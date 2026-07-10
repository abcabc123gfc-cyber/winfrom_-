using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day18
{
    internal class 事件
    {
        EventHandler EventHandler;
        public void AddEventHandler(EventHandler handler)
        {
            EventHandler += handler;
        }
        public delegate void EventHandler1(object sender,EventArgs eventArgs);
        //自定义事件
        //格式: 
        public delegate void Event(object sender, EventArgs e);
    }
}
