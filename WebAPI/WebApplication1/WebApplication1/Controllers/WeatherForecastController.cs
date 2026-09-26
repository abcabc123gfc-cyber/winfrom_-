

//MVC 模式  设计模式  Model  View   Controller 
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    //特性 给WeatherForecastController扩展功能
    [ApiController]
    //路由 主要是用来生成(映射) 接口路径 controller代表的是 控制器的名称
    
    [Route("[controller]")]

    //接口路径的组成:
    // https://localhost:7156/WeatherForecast/yubao1

    //协议+域名+端口/控制器的名称/接口名称

    //控制器:
    //1.命名Controller 结尾  约定
    //2.使用相关特性[ApiController]  [Route("[controller]")]  可以让APi具有路由的功能
    //3.继承控制器的基类

    //天气预报控制器
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //日志相关的
        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}


        //定义一个接口
        //请求方式:https://localhost:7061/WeatherForecast
        //HttpGet 特性 表示这个请求接口 为get请求, 并命名为GetWeatherForecast

        //  [HttpGet(Name = "GetWeatherForecast")]// 注意: 在最小webapi中 Name是不生效的

        //[HttpGet("yubao1")]
        //[HttpPost]  //post 请求
        //[HttpPut ]   
        //[HttpDelete]

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //Mock 假数据 模拟数据 将来可以从数据库中获取
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("test")]
        public IEnumerable<WeatherForecast> Get1()
        {
            //Mock 假数据 模拟数据 将来可以从数据库中获取
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
