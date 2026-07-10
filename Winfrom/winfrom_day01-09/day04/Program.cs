using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day04
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
            Application.Run(new FIle_prictice());
            _01_FileStream类 _01_FileStream类 = new _01_FileStream类();
            //_02_BinaryWriter_和_BinaryReader _01_FileStream类 = new _02_BinaryWriter_和_BinaryReader();
            //_01_FileStream类.BinaryWriterTest();
        }
    }
}
