using _02_对象的创建和保存.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace day05
{
    public partial class _04_JSON序列化 : Form
    {
        public _04_JSON序列化()
        {
            InitializeComponent();


            //JSON序列化_Test();
            //JSON_反序列化();
            //JSON序列化_Test_02();
            JSON_反序列化_02();
        }
        #region C# 序列化_原生

        private void JSON序列化_Test()
        {
            People people = new People()
            {
                Name = "吴亦凡",
                Age = 19,
                Sex = "男",
                Birthday = "1321"
            };

            //通用 JSON 序列化器
            //通过 `typeof(People)` ,操作目标是 People 这个类
            //才能反射读取字段、生成 JSON 结构。
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(People));
            dataContractJsonSerializer.WriteObject(new FileStream(@"../../../../读写文件区/1.json", FileMode.OpenOrCreate), people);



        }
        private void JSON_反序列化()
        {

            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(People));

            People p1 = dataContractJsonSerializer.ReadObject(new FileStream(@"../../../../读写文件区/1.json", FileMode.Open)) as People;
            richTextBox1.AppendText(p1.Name);
            richTextBox1.AppendText(p1.Age.ToString());

        }
        #endregion

        #region C# 序列化_第三方
        private void JSON序列化_Test_02()
        {
            People people = new People()
            {
                Name = "吴亦凡",
                Age = 19,
                Sex = "男",
                Birthday = "1321"
            };
            string str=JsonConvert.SerializeObject(people);
            richTextBox1.AppendText(str);
            File.WriteAllText(@"../../../../读写文件区/2.json", str);
        }
        private void JSON_反序列化_02()
        {
            string str = File.ReadAllText(@"../../../../读写文件区/2.json");
            People people = JsonConvert.DeserializeObject<People>(str);
            richTextBox1.AppendText(people.Name);
            richTextBox1.AppendText(people.Age.ToString());
        }
        #endregion
    }
}
