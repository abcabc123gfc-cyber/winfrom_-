using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace _08_反射的应用
{
    public partial class Form1 : Form
    {
        private readonly string filePath = Path.Combine(Environment.CurrentDirectory, "../../libs/ClassLibrary1.dll");

        private  object class1 = null;
        private  string typeName=string.Empty;
        Type type = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
  
            Assembly classAssembly = Assembly.LoadFile(filePath);
            typeName = ConfigurationManager.AppSettings["DAL"];
            class1 = classAssembly.CreateInstance(typeName);
            type = class1.GetType();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            var  ps = type.GetProperties();
            foreach (var p in ps)
            {
                if (p.Name=="Id")
                {
                  textBox1.Text=  p.GetValue(class1).ToString();
                }
                else if(p.Name=="Name")
                {
                    textBox2.Text = p.GetValue(class1).ToString();
                }
            }
        }
        

        
    }
}
