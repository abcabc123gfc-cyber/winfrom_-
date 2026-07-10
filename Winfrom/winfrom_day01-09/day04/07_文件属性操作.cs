using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day04
{
    internal class _07_文件属性操作
    {
        public void FileAttributeTest()
        {
            if (!File.Exists("1.txt"))
            {
                File.Create("1.txt");
            }
           
            FileInfo fileInfo = new FileInfo("1.txt");
            //获取文件特性
            Console.WriteLine(fileInfo.Attributes);
            //获取文件特性
            Console.WriteLine(File.GetAttributes("1.txt"));

            //设置文件特性
            //只读
            File.SetAttributes("1.txt", FileAttributes.ReadOnly);
            fileInfo.Attributes=FileAttributes.ReadOnly;

            //隐藏
            fileInfo.Attributes=FileAttributes.Hidden;
            File.SetAttributes("1.txt",FileAttributes.Hidden);

            //同时设置文件只读与隐藏
            fileInfo.Attributes=FileAttributes.ReadOnly | FileAttributes.Hidden;
            File.SetAttributes("1.txt",FileAttributes.ReadOnly | FileAttributes.Hidden);

            //获取文件名
            Console.WriteLine(fileInfo.Name);
            //获取文件完整的路径也文件名
            Console.WriteLine(fileInfo.FullName);
            //获取文件扩展名
            Console.WriteLine(fileInfo.Extension);
            //获取文件的大小
            Console.WriteLine(fileInfo.Length);
            //获取文件创建时间
            Console.WriteLine(fileInfo.CreationTime);
            //获取文件最后修改时间
            Console.WriteLine(fileInfo.LastWriteTime);
            //获取文件最后访问时间
            Console.WriteLine(fileInfo.LastAccessTime);
            //获取文件是否只读
            Console.WriteLine(fileInfo.IsReadOnly);
            //获取文件所在文件夹的名称
            Console.WriteLine(fileInfo.DirectoryName);
            
            

        }
    }
}
