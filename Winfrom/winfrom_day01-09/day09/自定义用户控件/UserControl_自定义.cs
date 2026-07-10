using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day09.自定义用户控件
{
    public partial class UserControl_自定义 : UserControl
    {
        [Description("自定义事件,手机信息,点击收集的时候触发")]
        public event EventHandler MyEvent;
        public UserControl_自定义()
        {
            InitializeComponent();
        }

        private string iD;
        public string ID
        {
            get {
                iD=textBox1.Text;
                return iD; }
            set { iD = value; }
        }

        private void UserControl_自定义_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyEvent?.Invoke(this, new EventArgs());
        }
    }
}
