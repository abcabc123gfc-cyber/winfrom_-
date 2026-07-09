namespace day02
{


    internal class Stack
    {
        // 结构体可以让变量存储多个数据(值类型)
        // 用法和性质上基本和类相同,你可以理解为结构是值类型的对象
        // 用结构体的使用分为两步 : 1.定义结构体 2.创建结构体对象
       

       
        Point Point=new Point();    
       public void Push(Point point)
       {

            point.X = 10;
       }
    }

    internal struct Point
    {
        public int X;
        public int Y;
    }

}

