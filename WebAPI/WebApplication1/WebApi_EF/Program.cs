
using Microsoft.EntityFrameworkCore;
using WebApi_EF.Contexts;

namespace WebApi_EF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region 添加EF框架
            builder.Services.AddDbContext<TextDBContext>(options =>
            {
                string? str = builder.Configuration["Configuration:Backend"];
                Console.WriteLine(str);
                //需要添加 Microsoft.EntityFrameworkCore.SqlServer 包
                options.UseSqlServer(str);

            });
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
