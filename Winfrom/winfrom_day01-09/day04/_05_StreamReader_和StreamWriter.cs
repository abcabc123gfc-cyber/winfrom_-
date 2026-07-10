using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day04
{
    internal class _05_StreamReader_和StreamWriter
    {
        public void StreamReaderTest()
        {
            using(StreamReader sr = new StreamReader("D:\\File\\File.txt"))
            {
                sr.ReadLine();
            }


            using (FileStream fs = new FileStream("D:\\File\\File.txt", FileMode.Open,FileAccess.ReadWrite))
            {
                using(StreamWriter sw = new StreamWriter(fs))
                {
                     sw.WriteLine("hello world");
                }
            }
        }
    }
}
