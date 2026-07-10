using BarcodeStandard;
using SkiaSharp;
using System;
using System.Drawing;
using System.IO;
using System.Reflection.Emit;
using System.Windows.Forms;
using 示例.BLL;


namespace UI
{
    public partial class Login2 : Form
    {
       
        UserBLL userBLL = new UserBLL();      
        Image image = null; 
        Image pImage = null;
        Bitmap bmp = null;
        Color[] cor = { Color.Blue, Color.Yellow, Color.Black, Color.Red };

        Random rnd = new Random();
        string str = null;
        public Login2()
        {
            InitializeComponent();
            Form1_Load();
            Date_Test();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            register register = new register();
            register.Show();
            
        }

        private void Form1_Load()
        {
            label1.Text = "登录账号";
            label2.Text = "登录密码";
            label3.ForeColor = cor[rnd.Next(0, 4)];
            Text = "登录界面";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text.Trim() == "登录" && login())
            {
         
                UI.StudentList form2 = new UI.StudentList(userBLL.Personalinfo().Grade);
                this.Hide();
                //保存 条码 账户
                string str = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileDate = "_登录时间账户" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                string pathFile = Path.Combine(str, fileDate);
                image.Save(pathFile, System.Drawing.Imaging.ImageFormat.Png);
                //保存密码
                string fileDate1 = "_登录密码账户" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                string pathFile1 = Path.Combine(str, fileDate1);
                image.Save(pathFile1, System.Drawing.Imaging.ImageFormat.Png);
                form2.Show();
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
            }


        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public bool login()
        {

            if (textBox1.Text == "" || textBox2.Text == "")
            {
               
                return false;
            }
            if (string.IsNullOrEmpty(uiTextBox1.Text.Trim()) || !str.Equals(uiTextBox1.Text))
            {
                
                return false;
            }
            try
            {
                if (userBLL.Login(int.Parse(textBox1.Text), int.Parse(textBox2.Text)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;

            }


        }

        /// <summary>
        /// 游客登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label3_Click(object sender, EventArgs e)
        {
            
            UI.StudentList form2 = new UI.StudentList("游客登录");
            this.Hide();
            form2.ShowDialog();
        }

        DateTest dateTest = new DateTest();
        private void Date_Test()
        {
            dateTest.Date();
        }
      



        private void Login1_MouseLeave(object sender, EventArgs e)
        {



        }

        private void Login1_MouseEnter(object sender, EventArgs e)
        {


        }

        private void Login1_Load(object sender, EventArgs e)
        {

            textBox1.Text = "1";
            textBox2.Text = "1";


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = img();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                return;
            }
            string account = textBox1.Text.Trim();
            //string password = textBox2.Text.Trim();
            var barcode = new Barcode();

            SKImage sKImage = barcode.Encode(BarcodeStandard.Type.Code128, account, SKColors.Black, SKColors.White, 200, 100);
            using (SKData date = sKImage.Encode())

            using (MemoryStream ms = new MemoryStream(date.ToArray()))
            {
                image = Image.FromStream(ms);
                pictureBox1.Image = image;


            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                return;
            }
            string account = textBox1.Text.Trim();
            //string password = textBox2.Text.Trim();
            var barcode = new Barcode();

            SKImage sKImage = barcode.Encode(BarcodeStandard.Type.Code128, account, SKColors.Black, SKColors.White, 200, 100);
            using (SKData date = sKImage.Encode())

            using (MemoryStream ms = new MemoryStream(date.ToArray()))
            {
                pImage = Image.FromStream(ms);
                pictureBox2.Image = image;


            }
        }

        private void Login1_Load_1(object sender, EventArgs e)
        {
           
            pictureBox3.Image = img();
            uiTextBox1.Text = str;
        }
        private Bitmap img()
        {
            str = null;
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToString(rnd.Next(0, 9));
            }
            bmp = new Bitmap(100, 50);

            Graphics graphics = Graphics.FromImage(bmp);
            for (int i = 0; i < 5; i++)
            {

                graphics.DrawString(str[i].ToString(), new Font("Arial", rnd.Next(7, 15)), new SolidBrush(cor[rnd.Next(0, 4)]), (i) * 17, 0);
            }
            for (int i = 0; i < 10; i++)
            {
                graphics.DrawLine(new Pen(cor[rnd.Next(0, 4)]), new Point(rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height)), new Point(rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height)));

            }
            for (int i = 0; i < 50; i++)
            {
                bmp.SetPixel(rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height), cor[rnd.Next(0, 4)]);
            }

            return bmp;
        }
    }
}
