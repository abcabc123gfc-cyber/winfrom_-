using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TemperatureControl.WinForms.Forms;
using TemperatureControl.WinForms.Froms;
using TemperatureControl.WinForms.Model;

namespace TemperatureControl.WinForms
{
    public partial class MainFrm : Form
    {
        bool isMax = false;
        string[] _menuButtons;
        bool isMove = false;
        public static event EventHandler MyLoadEvent;
        private Dictionary<string, Func<Form>> _formFactory = new Dictionary<string, Func<Form>>()
        {
            { "用户类型", () => new UserType() },
            { "用户管理", () => new User() },
            { "仓库管理", () => new Warehouse() },
            { "分区管理", () => new Partition() },
            { "产品管理", () => new Product() },
            { "入库管理", () => new Inbound() },
            { "出库管理", () => new Outbound() },
            { "库存盘点", () => new InventoryCheck() },
            { "温控管理", () => new Temperature() },
           
        };
        public MainFrm()
        {
            InitializeComponent();
        }
        #region 初始化按钮 图标 与按钮事件
        private void MainFrm_Load(object sender, EventArgs e)
        {
            panel2.Width = 200;
            MainFrm_Resize(sender, e);
            #region 初始化  最大 最小 关闭 按钮
            pictureBox2.Image = Properties.Resources.max;
            pictureBox3.Image = Properties.Resources.min;
            pictureBox1.Image = Properties.Resources.close;
            #endregion
            #region 添加导航按钮
            _menuButtons = new string[]
{
    "用户类型",
    "用户管理",
    "仓库管理",
    "分区管理",
    "产品管理",
    "入库管理",
    "出库管理",
    "库存盘点",
    "温控管理",
    "系统锁屏"
};
            panel2.Controls.Clear();
            for (int i = 0; i < _menuButtons.Length; i++)
            {
                Button button = new Button()
                {
                    Text = _menuButtons[i],
                    Location = new Point(0, i * 80),
                    Width = panel2.Width,
                    Image = Properties.Resources.setting,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextAlign = ContentAlignment.MiddleRight,
                    Height = 80,
                    Font = new Font("微软雅黑", 10),
                    BackColor = ColorTranslator.FromHtml("#013E99"),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    //Tag = 可以存储窗体, object类型
                    FlatAppearance = { BorderSize = 0 }
                };
                button.Click += button_Click;
                panel2.Controls.Add(button);

            }

            #endregion
            #region 初始化 状态栏
            toolStripStatusLabel2.Text=UserINfoState.UserInfo.UserTypeName;
            #endregion

        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            //MessageBox.Show(button.Text);
            if (!_formFactory.TryGetValue(button.Text, out var factory))
            {
                return;
            }
            foreach (Control c in panel3.Controls)
            {

                c.Dispose();
            }
            label3.Text = button.Text;
            panel3.Controls.Clear();
            Form form = factory();
            // 关键：作为子控件嵌入
            form.TopLevel = false;
            //form.Width = panel3.Width;
            //form.Height = panel3.Height;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            //MyLoadEvent += form.lo

            panel3.Controls.Add(form);
            form.Show();
        }
        #endregion
        #region 初始化  最大 最小 关闭 按钮

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["Form1"];
            if (form != null)
            {
                form.Show();
                this.Close();
            }
            else
            {
                Application.Exit();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (isMax)
            {
                this.WindowState = FormWindowState.Normal;
                //强制重绘窗体，解决最大化后窗体不刷新问题
                MainFrm_Resize(sender, e);
                isMax = false;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;

                MainFrm_Resize(sender, e);
                isMax = true;
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.close;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            pictureBox1.Image = Properties.Resources.close2;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.max;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = Properties.Resources.max2;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.min;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {

            pictureBox3.Image = Properties.Resources.min2;
        }
        #endregion

       
        #region 初始化位置 标题栏 panel 1 与 图片按钮 panel 3 panel 4
        private void MainFrm_Resize(object sender, EventArgs e)
        {

            #region 初始化位置 标题栏 panel 1 与 图片按钮 位置
            pictureBox2.Location = new Point(panel1.Width - pictureBox2.Width - 20 - pictureBox1.Width, panel1.Height / 2 - 20);
            pictureBox1.Location = new Point(panel1.Width - 20 - pictureBox1.Width, panel1.Height / 2 - 20);
            pictureBox3.Location = new Point(panel1.Width - pictureBox2.Width - 20 - pictureBox1.Width - pictureBox3.Width, panel1.Height / 2 - 20);
            label1.Location = new Point(panel1.Width / 2 - label1.Width / 2, panel1.Height / 2 - label1.Height / 2);
            pictureBox4.Location = new Point(20, panel1.Height / 3 - 10);
            #endregion
            #region 初始化位置 导航 panel 2
            panel2.Location = new Point(0, panel1.Height);
            panel2.BackColor = ColorTranslator.FromHtml("#013E99");
            panel2.Height = this.Height - panel1.Height - statusStrip1.Height;

            #endregion

            #region 初始化 数据 panel 3
            panel3.Width = this.Width - panel2.Width;
            panel3.Height = this.Height - panel1.Height - statusStrip1.Height - panel4.Height;
            //panel3.BackColor = Color.Red;
            panel3.Location = new Point(panel2.Width, panel1.Height + panel4.Height);

            #endregion
            #region 初始化panel 1
            panel1.Location = new Point(0, 0);
            panel1.Width = this.Width;
            //MessageBox.Show($"重新绘图 panel1 宽度 {panel1.Width}");
            panel1.BackColor = ColorTranslator.FromHtml("#6B6FD5");
            #endregion

            #region 初始化 panel 4
            panel4.Location = new Point(panel2.Width, panel1.Height);
            panel4.BackColor = ColorTranslator.FromHtml("#013E99");
            panel4.Width = this.Width - panel2.Width;
            panel4.Height = panel1.Height;
            pictureBox5.Location = new Point(20, panel4.Height / 2 - 20);
            #endregion
            

        }
        #endregion
        #region 控制导航栏显示与隐藏
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (!isMove)
            {
                pictureBox5.Image = Properties.Resources.toggle_left;
                isMove = true;
                panel2.Width = 60;
                MainFrm_Resize(sender, e);
            }
            else
            {
                pictureBox5.Image = Properties.Resources.toggle_right;
                isMove = false;
                panel2.Width = 200;
                MainFrm_Resize(sender, e);
            }
        }
        #endregion
    }
}
