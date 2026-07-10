using System;
using System.Windows.Forms;

namespace day02
{
    public partial class 消息提示 : Form
    {
        public 消息提示()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //消息提示框
            //MessageBox.Show("这是消息提示框");
            //MessageBox.Show("这是消息提示框", "标题");
            //参数3:按钮
            //DialogResult dia = MessageBox.Show("这是消息提示框", "标题", MessageBoxButtons.OKCancel);
            //点击按钮后,会会返回 DialogResult 类型的枚举
            //if (dia == DialogResult.OK)
            //{
            //    MessageBox.Show("点击了确定");
            //}
            //按钮值:
            //    public enum MessageBoxButtons
            //{
            //    OK,
            //    OKCancel,
            //    AbortRetryIgnore,
            //    YesNoCancel,
            //    YesNo,
            //    RetryCancel
            //}

            #region 消息提示框 参数4: 图标
            MessageBox.Show("这是消息提示框", "标题", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            //    public enum MessageBoxIcon
            //{
            //    None = 0,
            //    Hand = 16,
            //    Question = 32,
            //    Exclamation = 48,
            //    Asterisk = 64,
            //    Stop = 16,
            //    Error = 16,
            //    Warning = 48,
            //    Information = 64
            //}
          
            #endregion
        }
    }
}
