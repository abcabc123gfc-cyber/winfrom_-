using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace day12
{
    internal class practice
    {
        
     

    }
    #region 汽车类继承
    public class Car
    {
        public readonly string Brand;
        public string Model { get; set; }
        string Color { get; set; }
        public Car(string brand, string model, string color)
        {
            Brand = brand;
            Model = model;
            Color = color;
        }

        public Car(string brand, string model) : this(brand, model, "白色")
        {


        }


    }

    public class ElectricCar : Car
    {
        public string BatterySize { get; set; }
        public ElectricCar(string brand, string model, string batterySize) : base(brand, model)
        {
            BatterySize = batterySize;
        }
        public void Show()
        {
            Console.WriteLine("品牌：{0}  型号：{1} 电池大小：{2}", Brand, Model, BatterySize);
        }

    }
    #endregion
    #region 学校类继承
    class Book
    {
        public readonly int isbn;
        public const string LibraryName = "市立图书馆";
        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public Book(int isbn, string title, string author, int price)
        {
            this.isbn = isbn;
            Title = title;
            Author = author;
            Price = price;
        }
        public void ShowInfo()
        {
           Console.WriteLine($"地区:{LibraryName},国际标准书号:{isbn},《{Title}》作者为{Author}，价格为{Price}元");
        }

    }
    class AudioBook:Book
    {
        double Duration { get; set;}
        public AudioBook(int isbn, string title, string author, int price, double duration) : base(isbn, title, author, price)
        {
            Duration = duration;
        }
        public void Show()
        {
            ShowInfo();
            Console.WriteLine("阅读时长"+Duration);
        }
    }
    #endregion
    #region 计算类
    class Calculator
    {
        public static void Add(int a, int b)
        {
            Console.WriteLine(a + b);
        }
        public static void Add(int a,int b,int c)
        {
            Console.WriteLine(a + b + c);
        }
        public static void Add(double a, double b)
        {
            Console.WriteLine(a + b);
        }

        public static void Add(params int[] arr)
        {
            int Sum = 0;
            foreach (var item in arr)
            {
                Sum+=item;
            }
            Console.WriteLine(Sum);
        }
    }
    #endregion


}
