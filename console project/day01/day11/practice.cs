using System;

namespace day11
{
    internal class practice
    {

    }
    /// <summary>
    /// 商品类
    /// </summary>
    class Product
    {
        public const double TaxRate = 0.13;

        private int _productID;
        public int ProductID { get { return _productID; } }

        public string Name { get; set; }
        public int Price { get; set; }

        public Product(int productID, string name, int price)
        {
            _productID = productID;
            Name = name;
            Price = price;
        }
        public Product()
        { }
        public double GetTaxAmount()
        {
            return Price * TaxRate;
        }
    }

    /// <summary>
    /// 学生类
    /// </summary>
    class Student
    {
        public const string SchoolName = "阳光中学";
        public readonly int StudentID;

        string Name { get; set; }
        int Age { get; set; }

        Student(int ID, string name, int age)
        {
            StudentID = ID;
            Name = name;
            Age = age;

        }
        public Student()
        { }
        public void ShowInfo()
        {
            Console.WriteLine("学号：{0}，姓名：{1}，年龄：{2}", StudentID, Name, Age);
        }

    }

    // <summary>
    /// 载货机类
    /// </summary>
    class Aicract
    {
        public string sign { get; set; }
        public string model { get; set; }
        public string Color { get; set; }
        public string AffiliatedCompany { get; set; }
        public double FlightSpeed { get; set; }
        private int _passengerCapacity;
        public int PassengerCapacity { get { return _passengerCapacity; } set { _passengerCapacity = value > 0 ? value : _passengerCapacity; } }
        private int _flightModel;
        public int FlightModel { get { return _flightModel; } set { _flightModel = value != 400 || value != 200 || value != 100 ? _flightModel : value; } }

        public Aicract(string sign, string model, string color, string affiliatedCompany, double flightSpeed, int passengerCapacity, int flightModel)
        {
            this.sign = sign;
            this.model = model;
            this.Color = color;
            this.AffiliatedCompany = affiliatedCompany;
            this.FlightSpeed = flightSpeed;
            this.PassengerCapacity = passengerCapacity;
            this.FlightModel = flightModel;
            Console.WriteLine("载货机编号：{0}，载货机型号：{1}，载货机颜色：{2}，载货机所属公司：{3}，载货机载货能力：{4}，载货机飞行模式：{5}", sign, model, color, affiliatedCompany, passengerCapacity, flightModel);
        }
        Random r = new Random();
        public void RandomDemo()
        {
            int a = r.Next(1, 401);
            PassengerCapacity = a;
        }
        public void ShowInfo()
        {
            Console.WriteLine("载货机编号：{0}，载货机型号：{1}，载货机颜色：{2}，载货机所属公司：{3}，载货机载货能力：{4}，载货机飞行模式：{5}", sign, model, Color, AffiliatedCompany, PassengerCapacity, FlightModel);
        }

    }


}
    