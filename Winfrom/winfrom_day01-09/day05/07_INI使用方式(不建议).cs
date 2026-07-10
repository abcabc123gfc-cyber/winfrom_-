using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tool;

namespace day05
{
    public partial class _07_INI使用方式_不建议_ : Form
    {
        public _07_INI使用方式_不建议_()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IniAPI.INIWriteItems("../../../../读写文件区/3abc.ini", "相机1", "ip=127.0.0.1");
        }
    }
}
