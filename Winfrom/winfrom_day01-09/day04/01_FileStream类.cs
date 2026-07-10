using System;
using System.IO;

namespace day04
{
    internal class _01_FileStream类
    {


        //1. 创建文件流 FIlestream
        //参数1: 文件路径
        //绝对路径:完整路径 从盘符开始
        //相对路径: 相对于当前项目,当前文件的位置
        //只写文件名: 表示在当前目录下创建文件 bin/debug/下
        // ./ 表示当前目录  ../表示上一级目录 bin目录 ../../表示上上级目录


        //参数2: 枚举类型, 表示对这个文件进行操作 , 只能创建文件不能创建目录
        // public enum FileMode
        //{
        //    
        //    Create,创建
        //    Open,打开
        //    OpenOrCreate,  创建并打开
        //}


        //参数3: 枚举 表示对这个文件中的数据进行操作 省略参数3默认可读可写
        //public enum FileAccess
        //{

        //    Read = 1, //读
        //    Write = 2, //写
        //    ReadWrite = 3//读写
        //}
        FileStream fileStream = new FileStream("../../../../读写文件区/1.txt", FileMode.Create);

        //2.创建一个字节数组(缓冲区) 用于存放读取的数据
        //每次读取 5MB的数据存储到内存中
        byte[] buffer = new byte[1024 * 1024 * 5];
        
        public void FileCreat()
        {
            //3.开始读取文件的方法
            //参数1: 存放数据的字节数组
            //参数2:开始向字节数组中存放数据的位置
            //参数3: 从文件中读取数据的长度
            //返回值: 实际读取的有效字节数, 如果返回0,则表示文件读取完毕
            int num = fileStream.Read(buffer, 0, buffer.Length);
            //循环读取文件
            while (num > 0)
            {
                //4.将字节数组中的数据转换成字符串
                string str = System.Text.Encoding.UTF8.GetString(buffer, 0, num);
                //5.将字符串输出到控制台
                Console.WriteLine(str);
                //6.再次调用Read方法,继续读取文件
                num = fileStream.Read(buffer, 0, buffer.Length);
            }
            Console.WriteLine(num);

        }
        public void FileRead()
        {
            
        }
    }
}
