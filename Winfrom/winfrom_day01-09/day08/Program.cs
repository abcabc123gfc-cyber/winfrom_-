using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using day08.练习;
using day08.Properties;
namespace day08
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
            //Application.Run(new _01_Task());
            //练习 a = new 练习();
            Application.Run( new day08.练习.prictice());
        
        }
    }
}
