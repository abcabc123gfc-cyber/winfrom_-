using System;
using System.IO;

namespace day04
{
    internal class _02_BinaryWriter_和_BinaryReader
    {
        // " " 创建的字符串 \ 表示转义
        //使用 @" " 来表示路径
        public void BinaryWriterTest()
        {
            using (BinaryReader binaryReader = new BinaryReader(new FileStream(@"../../../../读写文件区/1.txt", FileMode.Open)))
            {
                //报错 非BinaryReader 写入string 不能使用Binary 读取
                //string str = binaryReader.ReadString();
                //Console.WriteLine(str);
            }

        }
        //二进制写入实例

    }
}
