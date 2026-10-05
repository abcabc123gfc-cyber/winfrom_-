using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using WebAPI_练习;
using WebAPI_练习.Model;

namespace MES项目练习.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly TextDBContext db;
        public AddressController(TextDBContext db)
        {
            this.db = db;
        }
        [HttpGet("GetIndex{id:int}")]
        public ObjectResult GetIndex(int id)
        {
            var data = db.AddressInfo.Where(x => x.Id == id).ToList();

            return Ok(new { Code = 200, Message = "查询成功", Data = data });
        }
        [HttpPost("Insert")]
        public ObjectResult GetInsert([FromBody] Address address)
        {
            db.AddressInfo.Add(address);
            db.SaveChanges();
            return new ObjectResult(new { Code = 200, Message = "插入成功", Data = address });
        }
        [HttpPost("Update")]
        public ObjectResult GetUpdate([FromBody] Address address)
        {
            #region 方法1
            //db.Entry(address).State = EntityState.Modified;
            //if (db.SaveChanges() > 0)
            //    return new ObjectResult(new { Code = 200, Message = "更新成功", Data = data });
            //return new BadRequestObjectResult(new { Code = 400, Message = "更新失败", Data = data });
            #endregion

            var data = db.AddressInfo.Where(x => x.Id == address.Id).Single();
            data.Add = address.Add;
            data.State = address.State;
            //因为 data 已被跟踪，不需要 db.Update(data)
            //var v = db.Update(data);
            if (db.SaveChanges() > 0)
                return new ObjectResult(new { Code = 200, Message = "更新成功", Data = data });
            return new BadRequestObjectResult(new { Code = 400, Message = "更新失败", Data = data });
        }
        [HttpGet("GetSelect")]
        public ObjectResult GetSelect()
        {
            var data = db.AddressInfo.ToList();
            return new ObjectResult(new { Code = 200, Message = "查询成功", Data = data });
        }
        [HttpDelete("Address/{id:int}")]
        public ObjectResult GetDelete(int id)
        {
            var data = db.AddressInfo.Where(x => x.Id == id).Single();
            
            if (data == null)
                return NotFound(new { Code = 404, Message = "记录不存在", Data = data });
            db.AddressInfo.Remove(data);
            if (db.SaveChanges() > 0)
                return new ObjectResult(new { Code = 200, Message = "删除成功", Data = data });
            return new BadRequestObjectResult(new { Code = 400, Message = "删除失败", Data = data });
        }

    }
}
