using System;
using System.Threading;

namespace day16
{
    internal class 多播委托
    {
    }
    class Student1
    {
        public int ID { get; set; }
        public ConsoleColor PenColor { get; set; }
        public Student1(int id, ConsoleColor penColor)
        {
            this.ID = id;
            this.PenColor = penColor;
        }

        public void DoHomework()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.ForegroundColor = this.PenColor;
                Console.WriteLine("学生编号{0}正在写作业{1}", ID,i);
                Thread.Sleep(1000);
            }
        }
    }
}
