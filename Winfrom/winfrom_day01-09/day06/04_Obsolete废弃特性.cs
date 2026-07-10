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
    public partial class _04_Obsolete废弃特性 : Form
    {
        public _04_Obsolete废弃特性()
        {
            InitializeComponent();
        }
        // 默认是false 表示: 警告但是可以调用
        //[Obsolete("请使用debugTest1方法,本方法过时", false)]
        //可以加 true 强制不能调用,
        //[Obsolete("请使用debugTest1方法,本方法过时", true)]

        //标记:  属于 废弃的特性,
        [Obsolete("请使用debugTest1方法,本方法过时")]
        public void DebugTest2()
        {
            MessageBox.Show("3");
        }
    }
}
