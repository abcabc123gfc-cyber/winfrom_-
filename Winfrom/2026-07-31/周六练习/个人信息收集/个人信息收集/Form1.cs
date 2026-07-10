using Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace 个人信息收集
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cbProvince.SelectedIndex = 0;
            cbZZMM.SelectedIndex = 0;
            cbMZ.SelectedIndex = 0;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            // 窗体构造函数内写

            this.DoubleBuffered = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            //对控价段进行空值检测
            if (!INullCheck(this))
            {
                MessageBox.Show("请填写完整信息！");
                return;
            }
            //json 序列化写入
            WriteFile();


            MessageBox.Show("提交成功！");
        }
        /// <summary>
        /// 检测所有输入框是否为空
        /// </summary>
        /// <returns></returns>
        private bool INullCheck(Control control)
        {

            foreach (Control c in control.Controls)
            {
                if (control.HasChildren && !INullCheck(c)) return false;
                if (c is TextBox)
                {
                    if (string.IsNullOrEmpty(((TextBox)c).Text))
                    {
                        return false;
                    }
                }
                else if (c is ComboBox box)
                {// 模式匹配写法  作用:减少输入判断的次数
                    if (box.SelectedIndex == -1)
                    {
                        return false;
                    }
                }
                else if (c is DateTimePicker)
                {
                    if (((DateTimePicker)c).Value.ToString() == null)
                    {
                        return false;
                    }
                }
                else if (c is PictureBox)
                {
                    if (((PictureBox)c).ImageLocation == "")
                    {
                        return false;
                    }
                }
                else if (c is NumericUpDown)
                {
                    if (((NumericUpDown)c).Value == 0)
                    {
                        return false;
                    }
                }
                else if (c is RichTextBox richTextBox)
                {
                    if (string.IsNullOrEmpty(richTextBox.Text))
                    {
                        return false;
                    }
                }



            }
            return true;
        }

        private void WriteFile()
        {
            //string fileName = ConfigurationManager.AppSettings["Model"];
            //Assembly assembly = Assembly.LoadFrom(fileName);
            //var type = assembly.GetType("User");

            //ArrayList arrayList = new ArrayList() { };
            //List<> users = new List<type>()
            //{
            //    new User()
            //    {

            //    }
            //};\
            using (StreamWriter sw = new StreamWriter("User.json"))
            {
                sw.Write(JsonConvert.SerializeObject(new User()
                {
                    Name = textBox1.Text,
                    Residence = cbProvince.Text,
                    Sex = radioButton1.Checked ? '女' : '男',
                    Birth = dtpBirth.Value,
                    BirthAddress = txtBirthAddress.Text,
                    Political = cbZZMM.Text,
                    Party = dtpParty.Value,
                    Ethnicity = cbMZ.Text,
                    Weight = (double)nudWeight.Value,
                    Height = (double)nudHeight.Value,
                    Language = radioButton4.Checked ? "英语水平高" : radioButton3.Checked ? "英语水平中" : "英语水平低",
                    Contact = txtContact.Text,
                    Description = richTextBox1.Text,
                    PictureFIlePath = pictureBox2.ImageLocation


                }));
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //当点击图片控件时执行,弹出图片选择对话框 ,在用户点击确定显示图片
            //对图片进行重命名, 并移动到项目文件夹内
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "请选择图片";
                openFileDialog.Filter = "图片选择|*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)



                    //File.Copy(openFileDialog.FileName, "picture\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp");
                    pictureBox2.Load(openFileDialog.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader("User.json"))
            {
                string json=sr.ReadLine();
                User user = JsonConvert.DeserializeObject<User>(json);
                textBox1.Text = user.Name;
                cbProvince.Text = user.Residence;
                radioButton1.Checked = user.Sex == '女' ? true : false;
               
                dtpBirth.Value = user.Birth;
                txtBirthAddress.Text = user.BirthAddress;
                cbZZMM.Text = user.Political;
                dtpParty.Value = user.Party;
                cbMZ.Text = user.Ethnicity;
                nudWeight.Value = (decimal)user.Weight;
                nudHeight.Value = (decimal)user.Height;
                radioButton4.Checked = user.Language == "英语水平高";
                radioButton3.Checked = user.Language == "英语水平中";
                radioButton2.Checked = user.Language == "英语水平低" ? true : false;
                txtContact.Text = user.Contact;
                richTextBox1.Text = user.Description;
                pictureBox2.Load(user.PictureFIlePath);
               

            }
        }
    }
}

