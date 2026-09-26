using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        //https://localhost:7061/api/Home   
        //HomeController  以Controller 结尾
        public string Get1()
        {
            return "Hello World!";

        }
        [HttpGet("add")]
        public string GetAdd()
        {
            return "Hello World!";

        }
        [HttpGet("Test")]
        public IActionResult GetTest()
        {
            //return new JsonResult(new { name = "张三", age = 18 });
            return new ObjectResult(new { name = "张三", age = 18 });
            //return OkResult();
            //返回不同的状态码
            //return NotFoundResult(404);
            //重定向
            //return new LocalRedirectResult("https://www.baidu.com");
        }
        [HttpGet("Test1")]
        public IActionResult GetTest1()
        {
            //return new JsonResult(new { name = "张三", age = 18 });
            //return new ObjectResult(new { name = "张三", age = 18 });
            //return OkResult();
            //返回不同的状态码
            //return NotFoundResult(404);
            //重定向
            //return new LocalRedirectResult("/api/Home/add");
            //重定向的方法
            return RedirectToAction("/api/Home/add");
            // AcceptedAtActionResult == ObjectResult ==> ActionResult
            // 常用的result
            //new JsonResult();
            //new ObjectResult(); 
            // return new BadRequestResult();  // Error: response status is 400
            //return new NotFoundResult();  // 404
            //return new OkObjectResult("添加一个用户！");
            //return new ContentResult();  // 返回空字符串
            //return new EmptyResult();// 返回空字符串
            //return new OkResult();
            //return new ForbidResult();  // 403
            // return new UnauthorizedResult();  // 401
            //return new NoContentResult();
            //return new StatusCodeResult(200);

            // 重定向：不能跳转到Post,PUT,DELETE请求，重定向只能跳转到Get请求
            //return new RedirectResult("/api/WeatherForecast/GetUser");
            // 常用的方法
            //return Ok();  // 方法相当于new OkResult();
            //return NotFound();  // 方法相当于new NotFoundResult();
            //return BadRequest(); // 方法相当于new BadRequestResult();
            //return Forbid();  // 方法相当于new ForbidResult();
            //return Content("添加一个用户！"); // 方法相当于new ContentResult();
            //return Empty; // 方法相当于new EmptyResult();
            //return Redirect("/api/WeatherForecast/GetUser"); // 方法相当于new RedirectResult();
            //return Unauthorized();
        }


        //友好 url
        [HttpDelete("Delete{id}")]
        public IActionResult Delete(int id)
        {
            return Ok("OK");
        }
        [HttpDelete("Delete1{id:int}")]
        //[HttpDelete("Delete1{id:int?}")] 加问号表示id可以为空 :不用填写
        public IActionResult Delete1(int id)
        {
            return Ok("OK");
        }
       
        [HttpPost("UP{id}")]
        public IActionResult UP(int id)
        {
            return Ok($"OK{id}");
        }
        //FromBody 数据在请求体中 不在url中显示
        [HttpPost("se")]
        public IActionResult se([FromBody]int id)
        {
            return Ok($"OK{id}");
        }
        [HttpDelete("Delete1{id1:int}")]
        public IActionResult UP1(int id1)
        {
            return Ok($"OK{id1}");
        }
        //总结: 最小webapi中有三种传递参数方式
        //1.路径参数:{id} 也称为:占位符参数  友好url
        //2.查询参数:?id=xxx 也成为:"查询字符串参数 FromQuery
        //以上两种参数方式,都有一个缺点,请求的参数会直接暴露在url上面,不安全,比如一些敏感的信息,账号 密码... 使用;post请求  post请求的参数,是直接放到请求体中
    }

}

