using System;
using System.Windows.Forms;

namespace day02
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
            //消息提示 s = new 消息提示();
                //Application.Run(new listbox控件());
                Application.Run(new listImage控件());
                //Application.Run(new NumericUpDown控件());
                //Application.Run(new ComboBox());
                //Application.Run(new 单选框按钮());
            //var s1 = s.ShowDialog();
            //if (s1 == DialogResult.OK)
            //{
                
            //    //Application.Run(new 消息提示());
            //}
           
            //Application.Run(new Form1());
        }
    }
}
