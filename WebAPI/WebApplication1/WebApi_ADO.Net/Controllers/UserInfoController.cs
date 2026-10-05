using Microsoft.AspNetCore.Mvc;
using WebApi_ADO.Net.Services;

namespace WebApi_ADO.Net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoController : Controller
    {
        private readonly IUserInfo _userInfo;
        public UserInfoController( IUserInfo userInfo)
        {
            _userInfo = userInfo;
        }
        [HttpPost("Add")]
        public ObjectResult Add([FromBody] Model.UserInfo userInfo)
        {
            if (_userInfo.Add(userInfo))
            {
                return Ok(new
                {
                    Code = 200,
                    Msg = "添加成功",
                    Data = userInfo
                });
            }
            else
            {
                return BadRequest(new
                {
                    Code = 400,
                    Msg = "添加失败",
                    Data = userInfo
                });
            }
        }
    }
}
