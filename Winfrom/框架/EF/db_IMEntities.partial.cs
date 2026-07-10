using System;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace EF
{

    public partial class db_IMEntities
    {
        // 方案A：从环境变量读取密码（最安全，适合生产）
        public db_IMEntities()
            : base(BuildConnectionString())
        {
            //Environment.GetEnvironmentVariable("DB_IM_PASSWORD")
        }

        // 方案B：如果你只是想本地调试，临时硬编码（用完记得删！）
        // public db_IMEntities() : base(BuildConnectionString("你的实际密码")) { }

        private static string BuildConnectionString()
        {
            // 从环境变量中获取密码
            string password = Environment.GetEnvironmentVariable("DB_IM_PASSWORD");
            if (string.IsNullOrEmpty(password))
            {
               password = ConfigurationManager.AppSettings["DB_IM_PASSWORD"];
            }
            // 1. 从 App.config 读取不含密码的 EF 连接字符串
            string configConnectionString = ConfigurationManager.ConnectionStrings["db_IMEntities"].ConnectionString;

            // 2. 解析成 EntityConnectionStringBuilder
            var entityBuilder = new EntityConnectionStringBuilder(configConnectionString);

            // 3. 取出内部的 SQL 连接字符串，加上密码
            var sqlBuilder = new SqlConnectionStringBuilder(entityBuilder.ProviderConnectionString);
            sqlBuilder.Password = password; // 这里把密码补上

            // 4. 重新组合
            entityBuilder.ProviderConnectionString = sqlBuilder.ToString();

            return entityBuilder.ToString();
        }
    }
}