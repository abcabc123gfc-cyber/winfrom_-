using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using WebAPI_练习.Model;

namespace WebAPI_练习.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        //在Porgram.cs 中已经把SqlSugar的服务添加到了IOC容器中,所有在这里可以直接点 依赖注入
        public UserController(ISqlSugarClient sqlSugarClient) => this._sqlSugarClient = sqlSugarClient;
        //用户接口 https://localhost:7127/User/selectAll
        [HttpGet("selectAll")]
        public ObjectResult GetSelectAll()
        {
           
            return new ObjectResult(new { Code = 200, Message = "查询成功", Data = _sqlSugarClient.Queryable<UserInfo>().ToList() });
        }
    }
}
