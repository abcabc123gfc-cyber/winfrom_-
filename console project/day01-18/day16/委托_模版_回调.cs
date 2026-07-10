using System;

namespace day16
{
    internal class 委托_模版_回调
    {

    }

    #region 计算
    class Calculator
    {

        public void Report()
        {
            Console.WriteLine(" 我有三个方法");
        }
        public int Add(int a, int b)
        {
            return a + b;
        }
        public int Sub(int a, int b)
        {
            return a - b;
        }
        public int Mul(int a, int b)
        {
            return a * b;
        }
        public int Div(int a, int b)
        {
            return a / b;
        }
    }
    #endregion

    #region 简单工厂模式
    public interface IProductFactory
    {
        Product Make();
    }
    public class PizzaFactory : IProductFactory
    {
        public Product Make()
        {
            Product product = new Product();
            product.Name = "pizza";
            product.Price = 12;
            return product;
        }
    }
    public class CarFactory : IProductFactory
    {
        public Product Make()
        {
            Product product = new Product();
            product.Name = "car";
            product.Price = 100;
            return product;
        }
    }
    /// <summary>
    /// 记录程序运行状态
    /// </summary>
    public class Loger
    {
        //用户在使用程序的时候，记录下当前时间，记录下当前用户，记录下当前用户的操作
        //再出现问题的时候，可以查看日志，查看用户，查看用户操作，查看用户操作时间，查看用户操作结果

        public void Log(Product product)
        {
            Console.WriteLine("产品时间{0},产品名称{1},产品价格{2}", DateTime.UtcNow, product.Name, product.Price);
        }
    }
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    class Box
    {
        public Product Product { get; set; }
    }
    class WrapFactor
    {
        public Box WrapProduct(IProductFactory productFactory,Action<Product> logCallBlack)
        {
            Box box = new Box();
            Product product = productFactory.Make();
            box.Product = product;
            if (product.Price >50)
            {
                logCallBlack(product);
            }
            return box;
        }
        
    }

    
    #endregion
}
