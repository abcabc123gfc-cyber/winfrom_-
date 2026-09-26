
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.WebSockets;
using SqlSugar;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region 添加SQLsugar  ioc
            // 注册上下文：AOP里面可以获取IOC对象，如果有现成框架比如Furion可以不写这一行
            builder.Services.AddHttpContextAccessor();
            #endregion

            #region 注册SqlSugar用AddScoped
            string? conne = builder.Configuration.GetConnectionString("BankConnstring");
         
            //builder.Services.AddSingleton<ISqlSugarClient> 使用单例模式添加 
            //Scoped 作用域
            builder.Services.AddScoped<ISqlSugarClient>(s =>
            {
                //Scoped用SqlSugarClient 
                SqlSugarClient sqlSugar = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.SqlServer,
                    //读取连接字符串
                    ConnectionString = conne,
                    IsAutoCloseConnection = true,
                },
               db =>
               {});
                return sqlSugar;
            });
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwagger();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
