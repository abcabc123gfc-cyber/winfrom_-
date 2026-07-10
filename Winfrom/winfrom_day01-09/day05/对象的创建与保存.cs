using _02_对象的创建和保存.Model;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace day05
{
    public partial class 对象的创建与保存 : Form
    {
        public 对象的创建与保存()
        {
            InitializeComponent();
        }

        private void 对象的创建与保存_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            People people = new People()
            {
                Name = textBox1.Text,
                Age = int.Parse(textBox2.Text),
                Sex = textBox3.Text,
                Birthday = textBox4.Text
            };
           
            using (StreamWriter streamWriter = new StreamWriter(@"../../../../读写文件区/1.txt",true))
                {
                    streamWriter.WriteLine(people.Name);
                    streamWriter.WriteLine(people.Age);
                    streamWriter.WriteLine(people.Sex);
                    streamWriter.WriteLine(people.Birthday);
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamReader streamReader = new StreamReader(@"../../../../读写文件区/1.txt", Encoding.UTF8))
            {
                People people = new People()
                {
                    Name = streamReader.ReadLine(),
                    Age = int.Parse(streamReader.ReadLine()),
                    Sex = streamReader.ReadLine(),
                    Birthday = streamReader.ReadLine()
                    
                };
                textBox1.Text = people.Name;
                textBox2.Text = people.Age.ToString();
            }
        }
    }
}
