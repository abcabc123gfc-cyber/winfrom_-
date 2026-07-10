using EF框架_代码优先.Domains;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF框架_代码优先.Contexts
{
    //定义数据库上下文
    //1. 继承 DbContext
    //2. 添加数据库连接字符串, 在构造函数中
    //3. 定义数据集(数据表) 映射成实体类, 建议是虚拟的
    //4. 添加实体类映射

    //继承 DbContext 基类, 自定义的数据库上下文, EFUserModel,具备了交互的能力(查询 增加 保存 跟踪 )
    internal class EFUserModel:DbContext
    {
       
        //public EFUserModel()
        //    : base(BuildConnectionString())
        //{
        //    //Environment.GetEnvironmentVariable("DB_IM_PASSWORD")
        //}
        public EFUserModel()
        : base("name=connString")
        {
            //base: 代表父类的构造函数,
            //创建对象时, 会调用父类的构造函数
        }
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

        //表示数据库中的一张表或者视图
        //UserInfo 就是一个实例类, 对应一张表, 其中每个属性, 对应这数据库中表的列名
        //virtual 允许在运行的时候创建该属性的代理对象
        //以便后期支持延迟加载与动态追踪
        public virtual DbSet<UserInfo> UserInfo { get; set; }
    }
}
