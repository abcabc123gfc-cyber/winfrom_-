using System;
using System.Threading;
using System.Threading.Tasks;
namespace day16
{
    internal class Program
    {
        delegate int Cal(int a, int b);
        static void Main(string[] args)
        {
            //EventTest();
            //FormClick();
            //Console.ReadKey();
            CustomEvent();
        }
        static void 测试委托()
        {
            #region 委托
            Calculator calculator = new Calculator();
            //Action action = new Action(calculator.Report);
            //直接调用
            //calculator.Reportd();
            //间接调用
            //action();
            //action.Invoke();

            //Func<int, int, int> func = calculator.Add;
            //func += calculator.Mul;
            //Console.WriteLine(func.Invoke(1, 2));
            //Type type = func.GetType();
            //if (type.IsClass)
            //{
            //    Console.WriteLine("Func 是一种类型");
            //}

            //Cal cal = new Cal(calculator.Add);
            //Console.WriteLine(cal(1, 2));
            //cal.Invoke(1, 2);
            #endregion
            #region 使用接口重置委托
            IProductFactory pizzaFactory = new PizzaFactory();
            IProductFactory carFactory = new CarFactory();
            //包装
            WrapFactor wrapFactor = new WrapFactor();


            Loger loger = new Loger();
            Action<Product> logCallBlack = new Action<Product>(loger.Log);

            Box box = wrapFactor.WrapProduct(pizzaFactory, logCallBlack);
            Box box1 = wrapFactor.WrapProduct(carFactory, logCallBlack);
            Console.WriteLine(box.Product.Name);
            Console.WriteLine(box1.Product.Name);
            #endregion

            #region 单播/多播委托
            //一个委托封装一个方法的形式叫做单播委托
            //Console.Clear();
            Student1 student1 = new Student1(1, ConsoleColor.Red);
            Student1 student2 = new Student1(1, ConsoleColor.Cyan);
            Student1 student3 = new Student1(1, ConsoleColor.Yellow);
            //多播委托
            Action action = student1.DoHomework;
            Action action1 = student2.DoHomework;
            Action action2 = student3.DoHomework;
            //action+=student2.DoHomework;
            //action.Invoke();
            #endregion

            #region 线程
            //直接_ 同步执行
            //student1.DoHomework();
            //student1.DoHomework();
            ////间接_同步执行_Invoke();
            //action.Invoke();

            //使用委托_隐式异步调用
            //使用BeginInvoke,会为我们自动生成一个分支线程,在分支线程中调用它封装的方法
            //参数1: 异步调用的回调方法: 调用完之后采取的动作
            //action.BeginInvoke(null,null);
            //action1.BeginInvoke(null,null);
            //action2.BeginInvoke(null,null);


            #endregion
            #region 显式异步调用    Thread
            //Thread thread =new Thread(student1.DoHomework);
            //Thread thread1=new Thread(student2.DoHomework);
            //Thread thread2=new Thread(student3.DoHomework);
            //thread.Start();
            //thread1.Start();
            //thread2.Start();
            #endregion
            #region 显式异步调用 Task
            Task task = Task.Factory.StartNew(student1.DoHomework);
            Task task1 = new Task(action1);
            Task task2 = new Task(action2);
            task1.Start();
            task2.Start();
            Console.ReadKey();
            #endregion
        }

        static void EventTest()
        {
            事件 event1 = new 事件();
            event1.EventTset();
        }
        static void FormClick()
        {
            事件示例 sample = new 事件示例();
            sample.FormTest();
        }
        public static void CustomEvent()
        {
            //Customer customer = new Customer();
            //Waiter waiter = new Waiter();
            //customer.Order+=waiter.Action;
            //customer.Action();
            Customer1 customer1 = new Customer1();
            Waiter waiter = new Waiter();
            //customer1.Order += waiter.Action;
            customer1.Action();
            //customer1.PayTheBill();
        }

    }
}
