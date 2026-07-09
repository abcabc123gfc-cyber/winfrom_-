using System;

namespace common
{
    public class Test
    {
        public Test(string Nme, int Age)
        {
            this.Name = Nme;
            this.Age = Age;
        }
        public Test()
        {
        }
        public string Name { get; set;}
        public int Age { get; set;}
        public void SayHelllo()
        {
            Console.WriteLine("hello world");
        }
    }
    public   class Proson
    {
        public string Name;
        public int Age;
        public void SayHelllo()
        {
            Console.WriteLine("hello world");
        }
    }
    public class Subclass:Test, I1
    { 
        public new void SayHelllo()
        {
            Console.WriteLine("Test的子类");
        }
    }
    public interface I1
    {
        void SayHelllo();
    }
}
