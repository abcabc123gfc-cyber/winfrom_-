using System;

namespace day12
{
    internal class 运行算符的重载
    {
        //运算符的重载: 一元运算符不仅仅可以作用在数字之间的运算也可以作用在自定义的数据类型之间
        Box box = new Box();
        Box box1 = new Box();
        //计算
        public void Volume()
        {
            box1 += box;
            Console.WriteLine(box1.Value);

        }

    }
    public class Box
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Value { get; set; }
        public Box(int height, int width, int length)
        {
            Height = height;
            Width = width;
            Length = length;
            Value = Height * Width * Length;
        }
        public Box() { }


        //重新定义某个类的运算符, 
        //重载+运算符 计算两个盒子的体积
        //格式: public static Box operator +(Box box1, Box box2)
        //需要注意的是某些运算符必须成对重载

        public static Box operator +(Box box1, Box box2)
        {
            Box box = new Box();
            box.Value = box1.Value + box2.Value;
            return box;
        }
        public static bool operator <(Box box1, Box box2)
        {
            return box1.Value < box2.Value;
        }
        public static bool operator >(Box box1, Box box2)
        {
            return box1.Value > box2.Value;
        }
    }

}
