using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _06_常见特性
{
    public partial class UCList : UserControl
    {
        public UCList()
        {
            InitializeComponent();
        }


        [Category("吴亦凡")]
        [Description("吴亦凡的账号信息")]
        public  string wuyifan
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

    }
}
