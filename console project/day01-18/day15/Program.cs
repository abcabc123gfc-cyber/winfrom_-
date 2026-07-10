namespace day15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            索引器 s = new 索引器();
            s.Test();
            Student student = new Student();
            student[20]="hello";
            System.Console.WriteLine(student[20]);
        }
    }
}
