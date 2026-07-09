using System;
using System.Windows.Forms;

namespace timer
{
    public partial class isMDI : Form
    {
        public isMDI()
        {
            InitializeComponent();
        }

        private void 添加窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //添加窗体
            Form1 f1 = new Form1();
            f1.MdiParent = this;
            f1.Show();
            Form1 f2 = new Form1();
            f2.MdiParent = this;
            f2.Show();
            Form1 f3 = new Form1();
            f3.MdiParent = this;
            f3.Show();


        }

        private void 横向排列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //横向排列
            //this.LayoutMdi(MdiLayout.Cascade);
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void 纵向排列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
    }
}
