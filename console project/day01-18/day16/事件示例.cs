using System;
using System.Windows.Forms;
namespace day16
{
    internal class 事件示例
    {
        public void FormTest()
        {
            #region 1星 示例

            //事件拥有者 form
            //事件: form的 click 事件
            //事件的响应者: controler
            //事件处理器: FormClick
            //订阅事件  this.form.Click += this.FormClick;
            //
            //Form form = new Form();
            //Controler controler = new Controler(form);
            //form.ShowDialog();
            #endregion
            #region 2星 示例
            //事件拥有者 form
            //事件: form的 click 事件
            //事件的响应者: form
            //事件处理器: Action
            //订阅事件 form.Click += form.Action;
            //Form form = new Form();
            //form.Click += form.Action;
            //form.ShowDialog();
            //事件拥有者 myForm
            //事件: myForm 的 click 事件
            //事件的响应者: myForm
            //事件处理器: Func
            //订阅事件 myForm.Click += myForm.Func;
            //MyForm myForm = new MyForm();
            //myForm.Click += myForm.Func;
            //myForm.ShowDialog();
            #endregion

            #region 3星 示例
            //事件拥有者 button
            //事件: button 的 click 事件
            //事件的响应者:myform对象
            //事件处理器: ButtonClicked
            //订阅事件 button.Click += button.ButtonClicked;
            MyForm myForm = new MyForm();
            myForm.ShowDialog();

            #endregion

        }
    }
    #region 示例
    class Controler
    {
        private Form form;
        public Controler(Form form)
        {
            if (form != null)
            {
                this.form = form;
                this.form.Click += this.FormClick;
            }
        }

        private void FormClick(object sender, EventArgs e)
        {
            this.form.Text = DateTime.Now.ToString();
        }
    }
    #endregion
    static class Form1
    {
        public static void Action(this Form form, object sender, EventArgs e)
        {
            form.Text = "扩展方法";
        }
    }
    public class MyForm : Form
    {
        private TextBox TextBox;
        private Button Button;
        internal void Func(object sender, EventArgs e)
        {
            this.Text = DateTime.Now.ToString();
        }
        public MyForm()
        {
            this.TextBox = new TextBox();
            this.Button = new Button();
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.Button);
            this.Button.Click += this.ButtonClicked;
            Button.Text = "say hello";
            Button.Top = 100;
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            TextBox.Text = "hello Word";
        }
    }


}
