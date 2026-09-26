
using SqlSugar;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //此段代码主要使用的控制反转思想(IOC),把各种中间件对象的控制权交给构建器,构建器把对象交给应用程序



            //1.创建了一个构建器对象:主要负责构建当前应用程序的运行环境
            //WebApplication 应用程序对象  相当于winform 也有  Application
            //有个这个构建器 构建器在构建一个对象的时候,可以携带各种参数
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container. 添加服务到容器中

            //public IServiceCollection Services
            //控制器有两个职责: 1.接受请求 json\text  2.返回结果 json
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            //添加Swagger 服务器,必须启用服务才有效
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region 添加sqlsugar
            builder.Services.AddHttpContextAccessor();

            //注册SqlSugar
            //使用单例模式添加   瞬时模式 
            builder.Services.AddSingleton<ISqlSugarClient>(s =>
            {
                SqlSugarClient sqlSugar = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = DbType.SqlServer,
                    //从appsetting.json中读取连接字符串
                    ConnectionString = builder.Configuration.GetConnectionString("BankConnstring"),
                    IsAutoCloseConnection = true,
                },
               db => { });
                return sqlSugar;
            });
            #endregion
            //2.创建一个应用程序对象,负责启动应用程序 构建一下 "编译一下"
            var app = builder.Build();

            // Configure the HTTP request pipeline.  配置HTTP请求管道
            if (app.Environment.IsDevelopment()) //如果是开发环境
            {
                app.UseSwagger();  //UseXXXX 使用中间件
                app.UseSwaggerUI();//   UI 用于界面
            }

            app.UseHttpsRedirection();//添加https重定向

            app.UseAuthorization();//添加授权


            app.MapControllers();//使用控制器中间件 (让控制器生效)

            //3.启动应用程序
            app.Run();
        }
    }
}
