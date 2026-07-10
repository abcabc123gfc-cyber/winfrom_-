using System;
using System.Drawing;
using System.Windows.Forms;

namespace TemperatureControl.WinForms.Froms
{
    public partial class frmBase : Form
    {
        public frmBase()
        {
            InitializeComponent();
            
        }

        public virtual void frmBase_Load(object sender, EventArgs e)
        {
            var v = this.Parent;
            if (v != null)
            {
                panel1.Width = v.Width;
                panel1.Height = 75;
                panel1.Location = new Point(0, 0);
                panel2.Height = 100;
                dataGridView1.Width = v.Width;
                dataGridView1.Height = v.Height - panel1.Height - panel2.Height;
                dataGridView1.Location = new Point(0, 75);
                panel2.Width = v.Width;

                panel2.Location = new Point(0, v.Height - panel2.Height);
                //panel2.BackColor = Color.Red;
            }
        }

        private void frmBase_Load_1(object sender, EventArgs e)
        {
            //隐藏行头
            dataGridView1.RowHeadersVisible = false;
            //禁止多选
            dataGridView1.MultiSelect = false;
            //禁止用户编辑
            dataGridView1.ReadOnly = true;
            // 禁止用户调整行高
            dataGridView1.AllowUserToResizeRows = false;
            //禁止用户调整列宽
            dataGridView1.AllowUserToOrderColumns = false;
            //自动填满表格
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cbbPageSize.SelectedIndex = 1;

        }

        private void frmBase_Resize(object sender, EventArgs e)
        {

        }
    }
}
