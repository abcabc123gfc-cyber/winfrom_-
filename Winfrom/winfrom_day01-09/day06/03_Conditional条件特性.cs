//#define DEBUG // 定义宏DEBUG
//#define DEBU // 定义宏DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day06
{
    public partial class _03_Conditional条件特性 : Form
    {
        public _03_Conditional条件特性()
        {
            InitializeComponent();
            debugTest1("1");
            debugTest1();
            DebugTest2();
        }

        //只有当定义了DEBUG宏时，debugTest1方法才会被调用
        //注意: 全大写的 DEBUG 在vs有的默认定义, 所以这里定义的DEBUG macro是错误的, 
        //不执行 DEBUG 需要在项目中改成发布模式

        //      正确的定义 macro 是 DEBU
        //[Conditional("DEBUG")]
        [ConditionalAttribute("DEBU")]
        public void debugTest1(string name)
        {
            MessageBox.Show(name);
        }

        [ConditionalAttribute("DEBUG")]
        public void debugTest1()
        {
            MessageBox.Show("2");
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
