using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_EF.Contexts;
using WebApi_EF.Model;


namespace WebApi_EF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly TextDBContext db;
        //前提: 注意!!!
        //想要在某个控制器注入对象,此对象必须在IOC容器中
        //通过构造函数注入,把当前上下文对象注入到当前控制器
        public UserController(TextDBContext db)
        {
            this.db = db;
        }
        [HttpPost("Add")]
        public ObjectResult Add([FromBody]UserInfo userInfo)
        {
            //设置状态为添加
            db.Entry(userInfo).State=EntityState.Added;
            //保存到数据库
            int row =db.SaveChanges();
            if (row > 0)
            {
                return Ok(new
                {
                    Code = 200,
                    Msg = "添加成功",
                    Data = userInfo
                });
            }
               return Ok(new
                {
                    Code = 400,
                    Msg = "添加失败",
                    Data = userInfo
                });
        }
    }
}
