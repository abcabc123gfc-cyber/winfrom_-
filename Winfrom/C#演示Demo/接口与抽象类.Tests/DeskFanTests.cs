using C_演示Demo.单元测试应用;
//使用第三方库 Moq 测试
using Moq;
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
        [Fact]
        public void PowerLowerThanZero_Ok_Moq()
        {
            var work = new Mock<IPowerSupply>();
            work.Setup(ps => ps.GetPower()).Returns(() => 0);
            var fan=new DeskFan(work.Object);
            var expected = "desk fan work stop";
            var catual = fan.Work();
            Console.WriteLine(fan.Work());
            string.Equals(expected, catual);
        }
        [Fact]
        public void PowerLowerThanZero_Warning_Moq()
        {
            var work = new Mock<IPowerSupply>();
            work.Setup(ps => ps.GetPower()).Returns(() => 220);
            var fan = new DeskFan(work.Object);
            var expected = "warning";
            var catual = fan.Work();
            Console.WriteLine(fan.Work());
            Assert.Equal(expected, catual);
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