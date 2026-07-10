using System;
using System.Drawing;
using System.Windows.Forms;

namespace day02
{
    public partial class 单选框按钮 : Form
    {
        public 单选框按钮()
        {
            InitializeComponent();
            Control();
        }
        public void Control()
        {
            RadioButton radioButton5 = new RadioButton();
            radioButton5.Text = "选项1";
            Panel panel1 = new Panel();
            panel1.Size = new Size(60, 40);
            panel1.Controls.Add(radioButton5);

            this.Controls.Add(panel1);
            //单选框 checked 属性 是一个bool值 通过 格式化输出查看值
            Console.WriteLine(radioButton5.Checked.ToString());
            
        }
        //总结 radioButton 互斥 单选框
        //1. checked属性 值是一个bool值 true选中 false未选中
        //2. 同一组互斥, 同一组只有一个选中
        //3. 使用容器添加多组单选框, 容器内添加单选框 如:panel 
        //4. 当checkedChanged 事件发生时, 单选框的checked属性值发生改变

    }
}
