using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day16
{
    internal class 自定义事件_简化声明
    {
    }
    public class OrderEventArgs1 : EventArgs
    {
        public string OrderName { get; set; }
    }
    public delegate void OderEventHander(Customer1 sender, OrderEventArgs e);
    public class Customer1
    {
        //简化 事件声明
        public event OderEventHander Order;
        public event EventHandler o;
        public int Bill { get; set; }
        public void PayTheBill()
        {
            Console.WriteLine("支付了账单{0}", Bill);
        }
        public void walkIn()
        {
            Console.WriteLine("进店");
        }
        public void SitDown()
        {
            Console.WriteLine("坐下");
        }
        public void Think()
        {
            Console.WriteLine("想吃...");
            //使用了, order事件去判断,实际上在order的内部会调用被省略的委托字段来判断, 与属性字段类似
            if (Order != null)
            {
                OrderEventArgs e = new OrderEventArgs()
                {
                    DishName = "牛排",
                    Size = "middle"
                };
                Console.WriteLine("菜的名字是{0}", e.DishName);
                Console.ReadLine();
                Order.Invoke(this, e);
            }
        }
        public void Action()
        {
            Console.ReadLine();
            walkIn();
            SitDown();
            Think();
        }
    }
}
