using System.Drawing;
using System.Windows.Forms;

namespace day09.扩展控件
{
    internal class MyTextBox : TextBox
    {
        //vs没有提供扩展控件的模板,需要使用用户控件模板改写,
        //注意:改写之后会报错: 需要吧InitializeComponent函数中的

        // 
        //this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
        //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //this.Name = "MyTextBox";
        //this.Size = new System.Drawing.Size(754, 362);
        //this.ResumeLayout(false);


        //换成一下这一行
        //components = new System.ComponentModel.Container();

        //也可直接创建普通类来创建扩展控件
        public MyTextBox()
        {
            this.Text = "这是扩展的TextBox";
        }

        private Color myBackColor;
        public Color MyBackColor
        {
            get { return myBackColor; }
            set
            {
                myBackColor = value;
                this.BackColor = myBackColor;
            }
        }
    }
}
