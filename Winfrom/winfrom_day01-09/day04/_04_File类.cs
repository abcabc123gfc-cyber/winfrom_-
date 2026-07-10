using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day04
{
    internal class _04_File类
    {
        //1.Create() 用于在指定路径下,
        // FileStream fs = File.Create("1.txt");
        //2.Delete() 删除一个文件
        // File.Delete("1.txt");
        //3.Move()  移动 剪切 Ctrl+X
        // File.Move("aaa/1.txt","bbb/1.txt");
        //4.Copy() 复制 
        // File.Copy("bbb/1.txt", "aaa/1.txt");
        //5.Exists() 判断一个文件是否存在,如果存在返回true,不存在返回false
        // File.Exists("bbb/1.txt");

        public void FileTest()
        {
            // 方法的读写

            // WriteAllBytes ReadAllBytes 文件读写  操作的是字节数组 会覆盖文件
            //写入
            File.WriteAllBytes("1.txt", Encoding.UTF8.GetBytes("12334"));
            //读取
            string str=  Encoding.UTF8.GetString( File.ReadAllBytes("1.txt"));


            //  WriteAllLines ReadAllLines 文件读写 操作的是字符串数组(数组中每一项是一行)
            //写入
            File.WriteAllLines("1.txt", new string[] {"123","123"});
            //读取
            string[] strs=File.ReadAllLines("1.txt",Encoding.UTF8);

            //WriteAllText  ReadAllText 文件读取 操作的字符串
            //写入
            File.WriteAllText("1.txt", "123",Encoding.UTF8);
            //读取
            str=File.ReadAllText("1.txt",Encoding.UTF8);

          //追加
          File.AppendAllText("1.txt", "123",Encoding.UTF8);
          

        }
    }
}
