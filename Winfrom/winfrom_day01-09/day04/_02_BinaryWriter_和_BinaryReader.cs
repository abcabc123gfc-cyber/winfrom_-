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
        public void BinaryWriterTest2()
        {

            // "" 创建的字符串  \ 表示转义
            //建议: 使用@"" 创建路径字符串 
            //FileStream fileStream = new FileStream(@"../../Files/123.txt", FileMode.Create, FileAccess.Write);
            ////创建二进制写入实例
            //BinaryWriter bw=new BinaryWriter(fileStream);
            //bw.Write("吴亦凡");
            //bw.Close();
            //fileStream.Close();
            //fileStream.Dispose();

            FileStream fileStream = new FileStream(@"../../Files/123.txt", FileMode.Open, FileAccess.Read);
            //创建二进制写入实例
            BinaryReader br = new BinaryReader(fileStream);
            string info = br.ReadString();
            Console.WriteLine(info);
            br.Close();
            fileStream.Close();
            fileStream.Dispose();



        }
        //二进制写入实例

    }
}
