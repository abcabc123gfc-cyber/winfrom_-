using day09.练习;
using day09.自定义用户控件;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day09
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
            //Application.Run(new _02GDI_());
            //Application.Run(new day09.扩展控件.扩展控件());
            Application.Run(new 条形码());
        }
    }
}
