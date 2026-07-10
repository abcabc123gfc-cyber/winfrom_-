using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day08
{
    public partial class _01_Task : Form
    {
        System.Threading.Tasks.Task t1 = null;
        //CancellationTokenSource 主要是向 CancellationToken 发出一个信号: 取消信号 
        //怎样发送信号? cts.Token 生成一个唯一标识符, 通过cts. cancel() 方法发出一个取消的信号, cts.IsCancellationRequested 属性来判断 是否收到取消的信号

        CancellationTokenSource cts = new CancellationTokenSource();

        public _01_Task()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //如何实例化(多种实例化方案)
            //1. 使用new Task(); 默认不会自启动, 需要手动调用Start()方法启动
            //有两个重载
            //public Task(Action) { }
            //public  Task(Action<object> action, object state) { }

            //1. 传入Action委托
            //public Task(Action) { }
            // public delegate void Acton();

            #region 声明线程方式
            Action action = new Action(DoWord);
            t1 = new System.Threading.Tasks.Task(action);

            Action action1 = DoWord;
            System.Threading.Tasks.Task t2 = new System.Threading.Tasks.Task(action1);

            System.Threading.Tasks.Task t3 = new System.Threading.Tasks.Task(new Action(DoWord));

            System.Threading.Tasks.Task t4 = new System.Threading.Tasks.Task(DoWord);

            System.Threading.Tasks.Task t5 = new System.Threading.Tasks.Task(new Action(
                () => { }));

            System.Threading.Tasks.Task t6 = new System.Threading.Tasks.Task(() => { });
            #endregion
            #region 解释线程参数
            //public Task(Action<object> action, object state) { }
            //public delegate void Action<T>(T obj);
            //两个参数的构造函数, 第一个参数是Action<Object> 委托, 第二个参数是object 类型的参数, 这个参数会传递给Action<object> 委托
            #endregion
            #region 使用Action委托 向 线程传递参数
            //向分线程传递数据
            t1 = new System.Threading.Tasks.Task(obj =>
            {
                Console.WriteLine(obj);
            }, "张三");
            #endregion
            #region Task线程 第三种重载方式 
            //public Task(Action<object> action, object state, CancellationToken cancellationToken)

            //public struct CancellationToken() 结构
            //  cts.Token 取消的信号,当调用cts.Cancel()方法时,会向CancellationToken发出一个取消的信号,分线程可以通过 CancellationToken.IsCancellationRequested属性来判断是否收到取消的信号

            t1 = new System.Threading.Tasks.Task(DoWord, "文字", cts.Token);
            t1.Start();
            #endregion

            #region Task.Run() 方法
            //2. 使用Task.Run() 方法: 静态方法创建任务实例, 默认会自动启动
            //2.1  public static Task Run(Action action)
            //  Task.Run(new Action(DoWord));
            // Task.Run(DoWord);

            //无参构造函数
            //public static Task Run(Action action)
            System.Threading.Tasks.Task.Run(() => { });

            System.Threading.Tasks.Task.Run(new Func<int>(() =>
            {


                return 1;
            }));

            System.Threading.Tasks.Task.Run(() =>
            {
                return 1;
            });

            System.Threading.Tasks.Task.Run(new Func<int>(() =>
            {
                return 1;
            }));

            System.Threading.Tasks.Task.Run(() =>
            {
                return 1;
            });

            System.Threading.Tasks.Task.Run(new Func<System.Threading.Tasks.Task>(() =>
            {
                return new System.Threading.Tasks.Task(() => { });

            }));

            System.Threading.Tasks.Task.Run(() =>
            {
                return new System.Threading.Tasks.Task(() => { });
            });


            System.Threading.Tasks.Task.Run(new Func<Task<string>>(() =>
            {
                return new Task<string>(() =>
                {
                    return "1";
                });
            }));

            System.Threading.Tasks.Task.Run(() =>
            {
                return new Task<string>(() =>
                {
                    return "1";
                });
            });

            System.Threading.Tasks.Task.Run(() => new Task<string>(() => "1"));

            //返回值是 是 Task<int>
            //Task<int> res = Task.Run(() => new Task<int>(() => 1));
            Task<int> resd1 = System.Threading.Tasks.Task.Run(() => 1);

            //Task.Run(DoWord,cts.Token)

            //Task<T> 泛型任务 主要让任务能够返回结果
            Task<string> res = System.Threading.Tasks.Task.Run(() =>
            {
                return "1";
            });

            Task<string> strs = System.Threading.Tasks.Task.Run(new Func<Task<string>>(() =>
            {
                return new Task<string>(() =>
                {
                    return "1";
                });
            }));



            #endregion
            #region Task.Factory.StartNew() 方法
            //3. 使用Task.Factory.StartNew() 方法: 静态方法创建任务实例, 默认会自动启动
            //3.1  public static Task StartNew(Action action)
            //  Task.Factory.StartNew(new Action(DoWord));
            // Task.Factory.StartNew(DoWord);

            //无参构造函数
            //public static Task StartNew(Action action)


            System.Threading.Tasks.Task.Factory.StartNew(() => { });

            System.Threading.Tasks.Task.Factory.StartNew(new Func<int>(() =>
            {


                return 1;
            }));

            System.Threading.Tasks.Task.Factory.StartNew(() => { return "1"; });


            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                return new System.Threading.Tasks.Task(() => { });
            });
            #endregion

            #region Task 取消的属性判断
            //判断任务是否因为取消而结束, 如果任务没有被取消,则返回 false, 如果任务被取消,则返回 true

            #endregion
        }
        int i = 0;
        private void DoWord(object obj)
        {

            //cts.IsCancellationRequested 判断是否取消
            //也就是 cts.Cancel()方法被调用了, 会返回 true ,否则返回 false
            while (!cts.IsCancellationRequested)
            {
                Invoke(new Action(() =>
                {
                    label1.Text = i.ToString();
                    i++;
                }));
            }

        }
        private void DoWord()
        {



        }

        /// <summary>
        /// 测试取消任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            cts.Cancel();

        }
        /// <summary>
        /// 测试等待 任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            int j = 0;
            //Task.WaitAll()   等待
            System.Threading.Tasks.Task t100 = new System.Threading.Tasks.Task(() =>
            {
                while (i <= 100)
                {
                    Invoke(new Action(() =>
                    {
                        label1.Text = j.ToString();

                        j++;
                    }));
                }
            });
            t100.Start();
            //会暂停对应的任务, 1秒后继续执行
            t100.Wait(1000);
            label2.Text = "等待主线程";
        }
        /// <summary>
        /// Task .WaitAny()/Task .WaitAll()/
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            //Task.WaitAny()
            List<string> list = new List<string>();

            //存储任务的列表
            List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();

            TaskFactory taskFactory = System.Threading.Tasks.Task.Factory;

            tasks.Add(taskFactory.StartNew(() => { Console.WriteLine("查询用户数据"); }));

            tasks.Add(taskFactory.StartNew(() => { Console.WriteLine("查询客户数据"); }));

            tasks.Add(taskFactory.StartNew(() => { Console.WriteLine("查询地址数据"); }));

            //Task.WaitAny() 卡死主线程 等待任务列表中 任意一个任务完成
            //Task.WaitAny(tasks.ToArray());
            System.Threading.Tasks.Task.WaitAny(tasks.ToArray());
            Console.WriteLine("查询到数据,进行渲染");



        }
        /// <summary>
        /// Task .WhenAny()/Task .WhenAll()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();
            TaskFactory taskFactory = System.Threading.Tasks.Task.Factory;


            tasks.Add(taskFactory.StartNew(() => { Console.WriteLine("查询用户数据"); }));


            tasks.Add(taskFactory.StartNew(() => { Console.WriteLine("查询客户数据"); }));


            tasks.Add(taskFactory.StartNew(() => { Console.WriteLine("查询地址数据"); }));

            tasks.Add(taskFactory.StartNew(() =>
            {
                while (true)
                {

                }
            }));

            //Task.WhenAny() 与 waitAny() 类似, 但是会等待所有任务完成
            //区别在于 whenAny 不会卡死主线程 一般whenAny 需要配合 ContinueWith 来使用, 当任务列表中 任意一个 任务完成之后继续执行其他任务

            //Task.WhenAny(tasks.ToArray()).ContinueWith(task =>
            //{
            //    Console.WriteLine("查到数据,返回页面");
            //} )); 

            //当任务列表中 所有任务完成之后继续执行其他任务
            //Task.WhenAll(tasks.ToArray()).ContinueWith(task =>
            //{
            //    Console.WriteLine("查到数据,返回页面");
            //});
            System.Threading.Tasks.Task.WhenAny(tasks.ToArray()).ContinueWith(task =>
            {
                Console.WriteLine("查到数据,返回页面");
            });



        }
        /// <summary>
        /// factory .ContinueWhenAny()/factory.ContinueWhenAll()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            List<Task<string>> tasks = new List<Task<string>>();
            TaskFactory taskFactory = System.Threading.Tasks.Task.Factory;

            //tasks.Add(taskFactory.StartNew(() => { Console.WriteLine("查询用户数据"); }));
            //tasks.Add(taskFactory.StartNew(() => { Console.WriteLine("查询客户数据"); }));
            //tasks.Add(taskFactory.StartNew(() => { Console.WriteLine("查询地址数据"); }));
            //tasks.Add(taskFactory.StartNew(() =>
            //{
            //    while (true)
            //    {

            //    }
            //}));

            tasks.Add(taskFactory.StartNew(() =>
            {
                return "1";
            }));
            tasks.Add(taskFactory.StartNew(() =>
            {
                return "2";
            }));





            //ContinueWhenAny()  ===>   WhenAny()+ContinueWith()
            //taskFactory.ContinueWhenAny(tasks.ToArray(), ts =>
            //{
            //    Console.WriteLine("查到数据,渲染页面");
            //});
            //ContinueWhenAll()  ===>   WhenAll()+ContinueWith()

            taskFactory.ContinueWhenAll(tasks.ToArray(), ts =>
            {
                //ts为任务列表 返回的数据列表
                foreach (var item in ts)
                {
                    Console.WriteLine(item.Result);
                }
            });


        }
    }
}
