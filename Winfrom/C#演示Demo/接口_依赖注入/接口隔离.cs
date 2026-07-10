using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 接口_依赖注入
{
    // ---------- 接口 ----------

    internal interface IVehicle
    {
        void Run();
    }

    internal interface ITruck: IVehicle, IWeapon
    {
        
      
    }

    interface IWeapon
    {
        void Fire();
    }

    // ---------- 车辆类 ----------

    internal class Car : IVehicle
    {
        public void Run()
        {
            Console.WriteLine("Run");
            
        }
    }

    public class Truck : IVehicle
    {
        public void Run()
        {
            Console.WriteLine("Run");
        }
    }

    // ---------- 坦克类 ----------

    internal class HeavyTank : ITruck
    {
        public void Fire()
        {
            Console.WriteLine("开火");
        }

        public void Run()
        {
            Console.WriteLine("MediumTank ... Run");
        }
    }

    internal class LightTank : ITruck
    {
        public void Fire()
        {
            Console.WriteLine("开火");
        }

        public void Run()
        {
            Console.WriteLine("LightTank... Run");
        }
    }

    internal class MediumTank : ITruck
    {
        public void Fire()
        {
            Console.WriteLine("开火");
        }

        public void Run()
        {
            Console.WriteLine("MediumTank ... Run");
        }
    }

    // ---------- 程序入口与驱动类 ----------

    internal class 接口隔离
    {
        static void Main(string[] args)
        {
            Dirver dirver = new Dirver(new Car());
            Dirver dirver1 = new Dirver(new MediumTank());
            dirver.Drive();
            dirver1.Drive();
            
        }
    }

    class Dirver
    {
        public IVehicle _truck;

        public Dirver(IVehicle powerSupply)
        {
            this._truck = powerSupply;
        }

        public void Drive()
        {
            _truck.Run();
        }
    }
}