using System.Configuration;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace _02_Project.DBContext
{
    /// <summary>
    /// EF Core 设计时工厂：供 dotnet-ef 迁移工具在没有运行时 DI 的环境下创建 DbContext。
    /// 运行时由 App.xaml.cs 的 ServiceCollection 注入，此工厂仅在执行迁移命令时使用。
    /// </summary>
    public class SQLContextFactory : IDesignTimeDbContextFactory<SQLContext>
    {
        public SQLContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<SQLContext>()
                .UseSqlServer(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString)
                .Options;
           
            return new SQLContext(options);
        }
    }
}
