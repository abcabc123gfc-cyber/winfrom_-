using day07.线程池;
using System;
using System.Threading;
using System.Windows.Forms;
using day07._06_ThreadPool练习;
namespace day07
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new 创建分线程());
            //Application.Run(new _04_线程池());
            //Application.Run(new day07.Thread练习.Form1());
            Application.Run(new day07._06_ThreadPool练习._06_ThreadPool练习());
        }
    }
}
