using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day04
{
    internal class _08_文件路径操作
    {
        public void FilePathTest()
        {
            string dir = "D:\\Microsoft Visual Studio\\XiangMu\\Winfrom\\winfrom_day01-09\\day04\\";

            //获取文件或文件夹所在的目录
            Console.WriteLine(Path.GetDirectoryName(dir));
            //获取文件扩展名
            Console.WriteLine(Path.GetExtension(dir));
            //获取文件或文件夹的名称
            Console.WriteLine(Path.GetFileName(dir));
            //获取绝对路径
            Console.WriteLine(Path.GetFullPath(dir));
            //获取根路径 盘符名称
            Console.WriteLine(Path.Combine(dir,"1.tet"));
            //生成新路径
            Console.WriteLine(Path.Combine(new string[] {@"C:\User","123"}));
            //随机生成文件名
            Console.WriteLine(Path.GetRandomFileName());

            //获取当前路径 
            Console.WriteLine(Directory.GetCurrentDirectory());

        }
    }
}
