using _02_对象的创建和保存.Model;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace day05
{
    public partial class _03_二进制序列化 : Form
    {
        public _03_二进制序列化()
        {
            InitializeComponent();
            //BinaryWriterTest();

            BinaryReaderTest();
        }
        public void BinaryWriterTest()
        {
            People people = new People()
            {
                Name = "吴亦凡",
                Age = 19,
                Sex = "男",
                Birthday = "2026/0730",
            };
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(new FileStream(@"../../../../读写文件区/1.txt", FileMode.Create), people);
          

        }
        public void BinaryReaderTest()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            People people = (People)binaryFormatter.Deserialize(new FileStream(@"../../../../读写文件区/1.txt", FileMode.Open));
           richTextBox1.AppendText(people.Name);
        }

    }
}
