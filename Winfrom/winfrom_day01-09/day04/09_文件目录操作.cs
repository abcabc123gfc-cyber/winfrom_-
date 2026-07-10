using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day04
{
    internal class _09_文件目录操作
    {
        public void FileDirectoryTest()
        {
            //获取当前路径下, 所有的子文件
            string[] files=Directory.GetFiles(Environment.CurrentDirectory);

            //递归函数: 自己调用自己 
            //递归需要有终止条件, 没有终止条件, 是死循环
            void GetFiles(string dir)
            {
                //获取当前路径下, 所有的子文件
                string[] files1 = Directory.GetFiles(dir);
                foreach (var item in files1)
                {
                    Console.WriteLine(item);
                }
                //获取当前路径下, 所有的子目录
                string[] dirs = Directory.GetDirectories(dir);
                foreach (var item in dirs)
                {
                    GetFiles(item);
                }
            }

            //在当前目录 创建目录 目录就是文件夹
            Directory.CreateDirectory("D:\\Microsoft Visual Studio\\XiangMu\\Winfrom\\winfrom_day01-09\\day04\\day09");
            //删除空目录
            Directory.Delete("D:\\Microsoft Visual Studio\\XiangMu\\Winfrom\\winfrom_day01-09\\day04\\day09");

            //删除非空目录
            DirectoryInfo directoryInfo=new DirectoryInfo("D:\\Microsoft Visual Studio\\XiangMu\\Winfrom\\winfrom_day01-09\\day04\\day09");
            //删除目录及子目录下的所有文件 非空
            directoryInfo.Delete(true);

        }
    }
}
