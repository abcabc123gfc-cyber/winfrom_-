using _02_对象的创建和保存.Model;
using System.IO;

namespace day04
{
    internal class _06_XML序列化
    {
        People people = new People()
        {
            Name = "张三",
            Age = 18,
            Sex = "男",
            Birthday = "1999-01-01"
        };


        public void XMLTest()
        {

            using (FileStream FileStream = new FileStream("people.xml", FileMode.Create))
            {

            }
        }
    }
}
