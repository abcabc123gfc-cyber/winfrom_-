using System;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            prictice prictice = new prictice();
            //17 阶乘
            //int result = torial(2);
            //Console.WriteLine(result);
            //三角形
            //prictice.triangle();
            //日历
            //prictice.Calendar();    
            //prictice.Sheng();
            //prictice.Ticket();
            //prictice.Ticket();
            //prictice.DancePartner();
            //prictice.StackDeomo(); 
            //prictice.Poem();
            //prictice.PoemIsTrue();
            //prictice.InoutFormat();
            //prictice .IPIsTrue();
            prictice.BaseStrig();
        }
        //17阶乘
        int Sum = 1;
        public static int torial(int n)
        {


            return n != 1 ? n * torial(n - 1) : 1;
        }
    }
}
