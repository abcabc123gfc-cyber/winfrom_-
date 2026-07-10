using C_演示Demo.接口;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_演示Demo
{
    internal class Interface_实现 : IPhone
    {
        public void Dail()
        {
            Console.WriteLine("Dial..");
        }

        public void PickUp()
        {
            Console.WriteLine("PickUp..");
            
        }

        public void Receive()
        {
            Console.WriteLine("Receive..");
            
        }

        public void Seed()
        {
            Console.WriteLine("Seed..");
            
        }
    }

    class Phoneuser
    {
        public IPhone _phone;
        public Phoneuser(IPhone phone)
        {
            _phone = phone;
        }
        public void UsePhone()
        {
            _phone.Dail();
            _phone.PickUp();
            _phone.Receive();
            _phone.Seed();
        }
       
    }
}
