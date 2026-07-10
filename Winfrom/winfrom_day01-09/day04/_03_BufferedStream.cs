using System;
using System.IO;
using System.Text;

namespace day04
{
    internal class _03_BufferedStream
    {
        //将文件从 磁盘读入到内存中
        //如果数据本身就在内存
        //使用:MemoryStream stream = new MemoryStream()

        //缓冲区: 是内存中的字节块,用于存储数据,从而减少操作系统调用的次数,缓冲区可以提高读取与写入的性能
        //参数:
        //stream: 源文件流
        //bufferSize: 缓冲区大小,默认为8192字节
        //leaveOpen: 是否保持源文件流的打开状态,默认为false
        public void BufferedStreamTest()
        {
            using (BufferedStream bufferedStream = new BufferedStream(File.Create("1.txt")))
            {
                string str = "123";
                byte[] bytes=Encoding.UTF8.GetBytes(str);
                bufferedStream.Write(bytes, 0, bytes.Length);
                str=Encoding.UTF8.GetString(bytes);
                Console.WriteLine(str);
            }
        }
    }
}
