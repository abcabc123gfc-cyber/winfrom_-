#define log // 必须要在文件开头 开始调试 log 区块
//#undef log  // 必须要在文件开头 停止调试 log 区块
#define debug
#undef debug
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day06
{
    public partial class _02__预处理器指令 : Form
    {
        public _02__预处理器指令()
        {
            InitializeComponent();
        }
        private void Form()
        {      
#if log
            Console.WriteLine("调试");
#elif debug

#else
            Console.WriteLine("发布");
             
#endif
        }
    }
}
