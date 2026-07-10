using System;
using System.Threading;
using System.Windows.Forms;

namespace day07
{
    public partial class 创建分线程 : Form
    {
        public 创建分线程()
        {
            InitializeComponent();
            //方案1 : 允许跨线程调用控件, 不报错, 但是不推荐使用,原因: 可能会出出现不可预知的错误
            //设置为 false 对非法跨线程的调用不进行检测
            //CheckForIllegalCrossThreadCalls = false;
            // check :检测 for :为了 illegal: 非法 cross: 通过  thread:线程 call:调用 
            //CheckForIllegalCrossThreadCalls = true;

            //-----
            //Thread.CurrentThread 获取当前线程
            //Thread.CurrentThread.Name 给当前线程设置名称

            //Thread.CurrentThread.Name = "主线程";
        }
        /// <summary>
        /// 创建分线程_01
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // 创建分线程 流程: 创建线程=>传入委托类型的变量=>委托保存函数=>函数就是执行耗时任务的函数 

            //1. 实例化一个分线程 委托就是一个函数的数据类型
            //delegate void ThreadDelegate();

            //Thread 类 用于专门创建(实例化)和管理(启动与停止)线程
            ThreadStart threadStart = new ThreadStart(Thread_01);
            Thread thread = new Thread(threadStart);

            //thread.ThreadState 获取线程状态
            //ThreadStart 属性 是一个枚举类型标识线程的状态
            MessageBox.Show(thread.ThreadState.ToString());
            //Unstarted 线程未启动 但是已经创建

            //2. 启动分线程
            //启动分线程, 会执行一个分线线程的业务逻辑, 但是主线程中的业务逻辑代码,也在执行. 分线程与主线程时同时执行的,是并发的无序的

            thread.Start();

            //1. 问题1
            // 想让分线程中的代码执行完毕,在执行主线程中的代码 使用
            //thread.Join();
            //join() 阻塞当前线程, 直到分线程执行完毕. 不建议使用
            // 多线程是为了提升程序的执行效率,为了解决主线程阻塞问题 
            MessageBox.Show(thread.ThreadState.ToString());
            //Running 线程正在正常运行
        }

        private void Thread_01()
        {
            Console.WriteLine("按钮1 线程事件");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //创建分线程_02
            ThreadStart threadStart = func2;
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        private void func2()
        {
            Console.WriteLine("按钮2 线程");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //3. 实例化一个分线程
            Thread thread = new Thread(ThreadFunc3);
            thread.Start();
        }

        private void ThreadFunc3()
        {
            Console.WriteLine("按钮3 线程");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //4. 创建一个分线程
            Thread thread = new Thread(() => MessageBox.Show("按钮4 线程"));
            thread.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 向分线程中 传递参数
            string str = "按钮5 线程";
            //Thread() 函数有重载 有参无参你都可以创建
            //ParameterizedThreadStart public delegate void ParameterizedThreadStart(object obj); 有参数 没有返回值的委托

            Thread thread = new Thread(ThreadFunc4);
            thread.Start(str);
        }

        private void ThreadFunc4(object str)
        {
            //方案2: 把耗时任务放到线程中执行,而页面的更新操作,放到主线程中执行
            //Invoke 方法 会将分线程中的操作,放到主线程中执行,所以不会报错
            //Invoke() 在控件所在的线程(主线程) 执行一个委托,也就是执行一个函数,这个函数就是操作ui界面的函数
            Invoke(new Action(() =>
            {
                label1.Text = (str.ToString());
            }));
        }

        Thread Thread = null;
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            Thread = new Thread(ThreadFunc5);
            Thread.Name = "线程1";
            Thread.CurrentThread.Name = "线程2";
            Thread.Start();
        }

        private void ThreadFunc5()
        {
            //设置一个线程为后台线程
            // Thread.IsBackground = true;
            //默认值为false : 线程为前台线程
            // 建议带参数的线程 都设置为后台线程
            Thread.IsBackground = true;
            if (Thread.ThreadState == ThreadState.Background)
            {
                //MessageBox.Show("后台线程");
            }
            progressBar1.Maximum = 100;
            while (true)
            {
                Thread.Sleep(100);
                Invoke(new Action(() =>
                {
                    if (progressBar1.Value >= progressBar1.Maximum)
                    {
                        progressBar1.Value = 0;
                    }
                    progressBar1.Increment(1);
                    //progressBar1.Value += 1;

                }));
                //Invoke(new Action(
                //    () => label1.Text = "进度:" + progressBar1.Value + "%"

                //    ));

            }
        }

        /// <summary>
        /// 挂起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            //暂停线程,线程进入阻塞状态,但是线程还在运行,只是暂停了,不会释放资源,所以不推荐使用   现在可以继续使用
            Thread.Suspend();
        }
        /// <summary>
        /// 继续
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            //恢复线程,线程继续运行,但是不推荐使用 继续
            Thread.Resume();
        }
        /// <summary>
        /// 终止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            Thread.Abort();
            if (Thread.IsAlive)
            {
                MessageBox.Show("线程未结束");
            }
            else
            {
                progressBar1.Value = 0;
            }
        }
        /// <summary>
        /// 重启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            if (Thread == null)
            {
                button6_Click(sender, e);
            }
            if (Thread.ThreadState == ThreadState.Stopped)
            {
                Thread.Start();
            }
            else
            {
                Thread.Abort();
                if (!Thread.IsAlive)
                {
                    Thread = new Thread(ThreadFunc5);
                    Thread.Start();
                    Thread.IsBackground = true;
                    progressBar1.Value = 0;
                }
            }
        }
        /// <summary>
        /// 中断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            Thread.Interrupt();
        }
        /// <summary>
        /// 取消中断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            Thread.ResetAbort();
        }
        /// <summary>
        /// 线程属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Thread.CurrentThread.Name);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(new ThreadStart(Thread1));
            t1.Name = "t1";
            t1.Priority = ThreadPriority.Normal;

            Thread t2 = new Thread(new ThreadStart(Thread1));
            t2.Priority = ThreadPriority.Lowest;
            t2.Name = "t2";
            Thread t3 = new Thread(new ThreadStart(Thread1));
            t3.Name = "t3";
            t3.Priority = ThreadPriority.Highest;
            t1.Start();
            t2.Start();
            t3.Start();

        }

        private void Thread1()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("线程 _" + Thread.CurrentThread.Name);
            }
        }
        /// <summary>
        /// 执行多个任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            CallBackMultOperate(() => Console.WriteLine("任务1"), () => Console.WriteLine("任务2"));
        }
        private void CallBackMultOperate(Action v1,Action v2)
        {
            Thread thread = new Thread(() =>
            {
                v1.Invoke();
                v2.Invoke();
            });
            thread.Start();
        }
        //创建一个锁 互斥锁
        int num = 0;
       static object obj = new object();
        /// <summary>
        /// 线程锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            { 
                for(int i = 0;i < 10; i++)
                {
                    Thread.Sleep(100);
                    //一个资源被锁住之后, 只能被某个线程使用,其他的线程只能等待, 直到锁被释放, 才可以使用这个资源
                    //目的: 增强程安全 资源的安全性
                    //什么时候使用锁: 线程安全, 线程同步, 线程互斥
                    //读个线程同时访问一个资源,需要使用线程锁
                    lock (obj)
                    {
                        
                        Console.WriteLine("线程1: " + num++);
                    }
                }
            });
            //-----  线程2
            Thread thread_2 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    //一个资源被锁住之后, 只能被某个线程使用,其他的线程只能等待, 直到锁被释放, 才可以使用这个资源
                    //目的: 增强程安全 资源的安全性
                    //什么时候使用锁: 线程安全, 线程同步, 线程互斥
                    //读个线程同时访问一个资源,需要使用线程锁
                  

                        Console.WriteLine("线程2: " + num--);
                    
                }
            });
            thread.Start();
            thread_2.Start();

        }
    }
}
