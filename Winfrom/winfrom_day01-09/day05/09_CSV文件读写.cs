using CSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day05
{
    public partial class _09_CSV文件读写 : Form
    {
        public _09_CSV文件读写()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CSVApi.SaveData("文字1", "文字2", "文字3");
            //输出读入的内容
            string str=CSVApi.ReadData();
            MessageBox.Show(str);
        }
    }
}
