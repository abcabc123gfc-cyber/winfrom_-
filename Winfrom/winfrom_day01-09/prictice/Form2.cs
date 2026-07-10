using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prictice
{
    public partial class Form2 : Form
    {
        public Form2(string str ="游客界面")
        {
            InitializeComponent();
            this.textBox1.Text =str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form target = Application.OpenForms["Form1"];
           if (target != null )
            {
                Login1 form=target as Login1;
                form.Show();
                
            }
            this.Close();
            
        }
    }
}
