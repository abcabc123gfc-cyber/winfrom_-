using _02_对象的创建和保存.Model;
using _06_XML序列化.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace day05
{
    public partial class _06_XML序列化 : Form
    {
        public _06_XML序列化()
        {
            InitializeComponent();
            Xml序列化();
            //Xml反序列化();
            //Xml反序列化_集合();
            //Xml反序列化_集合("1");
            Xml反序列化_集合(1);

            //创建一个xml文档操作对象
            XmlDocument xmlDocument = new XmlDocument();
            //加载xml文档到文档对象中
            xmlDocument.Load("../../../../读写文件区/3.xml");
            //获取xml文档的根节点
            XmlNode xmlNode = xmlDocument.DocumentElement;

            //richTextBox1.AppendText(xmlNode.OuterXml);
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                //获取节点名称
                //Console.WriteLine(node.Name);
                //使用switch case 判断 name 来处理节点
                //麻烦 可以使用 反序列化
            }
            //获取所有节点的子节点
            //xmlNode.ChildNodes;

        }
        public void Xml序列化()
        {
            List<People> peopleList = new List<People>()
            {
                new People()
                {
                    Name = "吴亦凡",
                    Age = 19,
                    Sex = "男",
                    Birthday = "1999-09-09"
                },
                new People()
                {
                    Name = "王源",
                    Age = 18,
                    Sex = "男",
                    Birthday = "1999-09-09"
                },
                 new People()
                {
                Name = "吴亦凡",
                Age = 19,
                Sex = "男",
                Birthday = "2026/07/30",
                },
            };
            People people = new People()
            {
                Name = "吴亦凡",
                Age = 19,
                Sex = "男",
                Birthday = "2026/0730",
            };

            using (FileStream fs = new FileStream("../../../../读写文件区/4.xml", FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fs))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<People>));
                    xmlSerializer.Serialize(streamWriter, peopleList);
                }

            }
        }
        public void Xml反序列化()
        {
            string str = File.ReadAllText("../../../../读写文件区/3.xml");
            //richTextBox1.AppendText(str);

            using (FileStream fs = new FileStream("../../../../读写文件区/3.xml", FileMode.OpenOrCreate))
            {
                using (StreamReader sw = new StreamReader(fs))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(People));
                    People p1 = xmlSerializer.Deserialize(fs) as People;
                    //richTextBox1.AppendText(p1.Name);
                }

            }

        }
        public void Xml反序列化_集合()
        {
            using (StreamReader sr = new StreamReader("../../../../读写文件区/XMLFile1.xml", Encoding.UTF8))
            {
                //明天尝试一下,直接使用list序列化列表  会报错,需要加注释
                //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Students));
                Students students = xmlSerializer.Deserialize(sr) as Students;
                foreach (Student student in students.StudentList)
                {
                    Console.WriteLine(student.StuName);
                }

            }
        }
        /// <summary>
        /// 报错
        /// </summary>
        /// <param name="str"></param>
        public void Xml反序列化_集合(string str)
        {
            using (StreamReader sr = new StreamReader("../../../../读写文件区/XMLFile1.xml", Encoding.UTF8))
            {
                //明天尝试一下,直接使用list序列化列表
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
                List<Student> students = xmlSerializer.Deserialize(sr) as List<Student>;

                foreach (Student student in students)
                {
                    Console.WriteLine(student.StuName);
                }

            }
        }
        public void Xml反序列化_集合(int str)
        {
            //可以运行
            using (StreamReader sr = new StreamReader("../../../../读写文件区/4.xml", Encoding.UTF8))
            {
                // 使用list序列化xml, 使用list反序列化xml
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<People>));
                List<People> peoples = xmlSerializer.Deserialize(sr) as List<People>;

                foreach (People student in peoples)
                {
                    Console.WriteLine(student.Name);
                }

            }
        }

    }
}
