using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace prictice
{
    public partial class 轮播图 : Form
    {
        public 轮播图()
        {
            InitializeComponent();

        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            label1.Hide();
            label2.Hide();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            label1.Show();
            label2.Show();
        }

        private void 轮播图_Load(object sender, EventArgs e)
        {
            //pictureBox1.Image = Image.FromFile("D:\\1.jpg");
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {

        }
        int i = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            string str = @"C:\Users\Administrator\Downloads\" + (i++) + "_风景.bmp";
            pictureBox1.Image = Load1(str);
        }
        public Image Load1(string str)
        {
            if (!File.Exists(str))
            {
                str = @"C:\Users\Administrator\Downloads\" + 1 + "_风景.bmp";
                MessageBox.Show("图片不存在");
                i = 1;
            }

            using (Image img = Image.FromFile(str))
            {

                return new Bitmap(img);
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {
            i++;
            string str = @"C:\Users\Administrator\Downloads\" + (i++) + "_风景.bmp";
            pictureBox1.Image = Load1(str);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            i--;
            string str = @"C:\Users\Administrator\Downloads\" + (i++) + "_风景.bmp";
            pictureBox1.Image = Load1(str);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //checkBox2.Checked = false;
            if (checkBox1.Checked == true)
            {

                check_change(true, checkBox2);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //checkBox1.Checked = false;
            if (checkBox2.Checked == true)
            {

                check_change(false, checkBox2);
            }
        }
        /// <summary>
        /// 批量修改CheckBox
        /// </summary>
        /// <param name="isChecked"></param>
        /// <param name="checkBox1"></param>
        public void check_change(bool isChecked, CheckBox checkBox1)
        {
            foreach (Control c in this.Controls)
            {
                if (c is CheckBox && c.Name != checkBox1.Name)
                {
                    CheckBox checkBox = (CheckBox)c;
                    checkBox.Checked = isChecked;
                }
            }
        }

    }
}
