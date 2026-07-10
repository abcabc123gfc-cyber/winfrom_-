using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
namespace day12
{
    public partial class _01应用程序配置 : Form
    {
        public _01应用程序配置()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = ConfigurationManager.AppSettings["connString1"];
            //richTextBox1.Text = str;
            string config=ConfigurationManager.ConnectionStrings["connString2"].ConnectionString;
            richTextBox1.Text = config;
        }
    }
}
