using System;

namespace C_演示Demo.单元测试应用
{
   
  public  interface IPowerSupply
    {
        int GetPower();
    }

   public class PowerSupply: IPowerSupply
    {
        public int GetPower()
        {
            return 100;
        }
    }
    
    public  class DeskFan
    {
        private IPowerSupply _powerSupply;
        public DeskFan(IPowerSupply powerSupply)
        {
            _powerSupply = powerSupply;
        }
        public string Work()
        {
            int power = _powerSupply.GetPower();
          
            if (power <= 0)
            {
              return("desk fan work stop");

            }
            else if (power <= 100)
            {
                return ("desk fan work");
            }
            else if (power <= 200)
            {
                return ("desk fan work very well");
            }
            else
            {
                return ("warning");
            }
        }
    }

    
}
