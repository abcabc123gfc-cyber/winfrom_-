using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using WebAPI_练习.Model;

namespace MES项目练习.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        public UserController(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }
        [HttpGet("GetIndex{id:int}")]
        public ObjectResult GetIndex(int id)
        {
            return new ObjectResult(new { Code = 200, Message = "查询成功", Data = _sqlSugarClient.Queryable<UserInfo>().Where(x => x.Id == id).ToList() });
        }
        [HttpGet("GetName/{Name}")]
        public ObjectResult GetName(string Name)
        {
            return new ObjectResult(new { Code = 200, Message = "查询成功", Data = _sqlSugarClient.Queryable<UserInfo>().Where(x => x.Name.Contains(Name)).ToList() });
        }
        [HttpPost("Delete")]
        public ObjectResult GetIndexDelete([FromBody] int id)
        {
            return new ObjectResult(new { Code = 200, Message = "查询成功", Data = _sqlSugarClient.Deleteable<UserInfo>(x => x.Id == id).ExecuteCommand() });
        }
        [HttpGet("GetSelecte")]
        public ObjectResult GetSelecte()
        {
            return new ObjectResult(new { Code = 200, Message = "查询成功", Data = _sqlSugarClient.Queryable<UserInfo>().ToList() });
        }
        [HttpGet("GetUPdate&id/{id:int}&Name/{Name}&Account/{Account:int}&Password/{Password:int}&Grade/{Grade:int}&State/{State:int}")]
        public ObjectResult GetUPdate(int id, string Name, int Account, int Password, int Grade, int State)
        {
            return new ObjectResult(new { Code = 200, Message = "查询成功", Data = _sqlSugarClient.Updateable<UserInfo>(new UserInfo { Id = id, Name = Name, Account = Account, Password = Password, Grade = Grade, State = State }).ExecuteCommand() });
        }
        [HttpGet("GetInsert&Name/{Name}&Account/{Account:int}&Password/{Password:int}&Grade/{Grade:int}&State/{State:int}")]
        public ObjectResult GetInsert(string Name, int Account, int Password, int Grade, int State)
        {
            return new ObjectResult(new { Code = 200, Message = "查询成功", Data = _sqlSugarClient.Insertable<UserInfo>(new UserInfo { Name = Name, Account = Account, Password = Password, Grade = Grade, State = State }).ExecuteCommand() });
        }
        [HttpGet("GetLogin&Name{Account:int}&Password/{Password:int}")]
        public ObjectResult GetLogin(int Account, int Password)
        {
            UserInfo v = _sqlSugarClient.Queryable<UserInfo>().Where(x => x.Account == Account && x.Password == Password && x.State == 0).First();
           
            return new ObjectResult(v != null ? true : false);
        }
    }
}
