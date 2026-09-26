using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using WebApi_SqlSuger.Model;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController1 : Controller
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        public UserController1(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }
        [HttpGet("IndexSelect{id}")]
        public string GetSelect(int id)
        {
            return "value";
        }
        [HttpGet("select")]
        public ObjectResult GetSelect()
        {

           return  new ObjectResult( _sqlSugarClient.Queryable<UserInfo>().ToList());
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] UserInfo mode)
        {
            try
            {
                int row = await _sqlSugarClient.Insertable(mode).ExecuteCommandAsync();

                if (row > 0)
                {
                    return Ok(new
                    {
                        Code = 200,
                        Msg = "添加成功"
                    });

                }
                else
                {
                    return BadRequest(new
                    {
                        Code = 10000,
                        Msg = "添加失败"
                    });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    Code = 10000,
                    Msg = ex.Message
                });
            }

        }
    }
}
