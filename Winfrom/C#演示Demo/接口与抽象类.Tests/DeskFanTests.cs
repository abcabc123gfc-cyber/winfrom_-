using C_演示Demo.单元测试应用;

namespace 接口与抽象类.Tests
{
    public class DeskFanTests
    {
        [Fact]
        public void PowerLowerThanZero_Ok()
        {
            var fan=new DeskFan(new PowerSupplyLowerThanZero());
            var expected = "desk fan work stop";
            var catual=fan.Work();
            Console.WriteLine(fan.Work());
            string.Equals(expected,catual);
        }
    }
    class PowerSupplyLowerThanZero:IPowerSupply
    {
      

        public int GetPower()
        {
            return 0;
        }
    }
}