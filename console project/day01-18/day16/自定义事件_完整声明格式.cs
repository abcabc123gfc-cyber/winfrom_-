using System;

namespace day16
{
    internal class 自定义事件_完整声明格式
    {
    }
    /// <summary>
    /// 传递事件消息
    /// </summary>
    public class OrderEventArgs : EventArgs
    {
        //某个类的用途是作为EventArgs来使用,需要继承 EventArgs 基类
        public string DishName { get; set; }
        public string Size { get; set; }
    }

    //如果委托是为了某个事件去准备的需要在委托名字后使用EventHander作为后缀
    //参数1  点菜的顾客 
    //参数2: 有关于你点的这个菜的信息:菜名,分量等.需要自定义数据类型
    public delegate void OderEventHandler(Customer customer, OrderEventArgs orderEventArgs);
    public class Customer
    {
        //Customer 事件的拥有者

        //委托字段
        private OderEventHandler _orderEventHandler;
        //事件本身
        public event OderEventHandler Order
        {
            //事件添加器
            add
            {
                _orderEventHandler += value;
            }
            //事件移除器
            remove
            {
                _orderEventHandler -= value;
            }
        }
        public double Bill { get; set; }

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
            if (_orderEventHandler != null)
            {
                OrderEventArgs e = new OrderEventArgs()
                {
                    DishName = "牛排",
                    Size = "middle"
                };
                _orderEventHandler.Invoke(this, e);
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
    //事件订阅者
    public class Waiter
    {
        //事件处理器
        internal void Action(Customer customer, OrderEventArgs e)
        {
            Console.WriteLine("菜的名字是{0}", e.DishName);
            double pricce = 10;
            switch (e.Size)
            {
                case "small":
                    pricce = 10 * 0.5;

                    break;
                case "middle":
                    pricce = 10 * 0.8;

                    break;
                case "large":
                    pricce = 10 * 1.5;

                    break;
                default:
                    break;
            }
            customer.Bill += pricce;
        }
    }
}
