using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day01
{
    public partial class 事件 : Form
    {
        public 事件()
        {
            InitializeComponent();
        }
        public void button_1()
        {
            //双击控件,会自动生成最常用的事件处理程序,并在设计文件中生成绑定的代码
            // this.button1.Click += new System.EventHandler(this.button1_Click);

            //事件处理程序
            //参数1:触发这个事件的控件(现在咱们给三个Button都绑定了这个事件处理程序,)
            //也就意味着,wuyifan  luozhixiang  liyundi 这个三个button都可以触发这个事件
            //当点击的是wuyifan这个button 的时候  sender 就是wuyifan这个button
            //当点击的是luozhixiang这个button 的时候  sender 就是luozhixiang这个button
            //参数2: 事件对象  事件也是一个对象,其中包含事件的相关信息,比如鼠标点击的 xy坐标 如果键盘事件 当前按下的键位....
        }
    }
}
