using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day02
{
    public partial class conbox控件 : Form
    {
        public conbox控件()
        {
            InitializeComponent();
        }
        public void conbox控件_Load()
        {
            //下拉列表
            //添加数据
            this.comboBox1.Items.Add("张三");
            this.comboBox1.Items.AddRange(new string[] { "张三", "王五", "赵六" });
            //删除数据
            this.comboBox1.Items.Remove("张三");
            comboBox1.Size = new Size(50, 100);
            
            
        }
    }
}
