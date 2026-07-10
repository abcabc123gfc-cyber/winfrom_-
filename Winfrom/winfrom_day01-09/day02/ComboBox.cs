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
    public partial class ComboBox : Form
    {
        public ComboBox()
        {
            InitializeComponent();
            ComboBox_Load();
        }
        public void ComboBox_Load()
        {
            //ComboBox comboBox = new ComboBox()
            //{
            //    Location = new Point(100, 100),
            //    Size = new Size(100, 50),

            //};

            //添加数据
            comboBox1.Items.Add("张三");
            comboBox1.Items.Add("王五");
            comboBox1.Items.AddRange(new string[] { "张三", "王五", "赵六" });
          // 删除数据
          comboBox1.Items.Remove("张三");
            comboBox1.Size = new Size(50, 100);

            //this.Controls.Add(comboBox);
            //当选项改变之后发生
            comboBox1.SelectedIndexChanged += (s, e) =>
            {
                MessageBox.Show(comboBox1.SelectedIndex.ToString());
                MessageBox.Show(comboBox1.SelectedItem.ToString());
            };
        }
    }
}
