
using Microsoft.EntityFrameworkCore;

namespace WebAPI_练习
{
    public class TextDBContext:DbContext
    {
        //如何获取数据库连接字符串
        //options 参数从哪传递过来的, 从Program.cs中的  builder.Services.AddDbContext() 方法传递过来的
        public TextDBContext(DbContextOptions<TextDBContext> options) : base(options)
        {

        }
        public virtual DbSet<WebAPI_练习.Model.Address> AddressInfo { get; set; }
    }
}
