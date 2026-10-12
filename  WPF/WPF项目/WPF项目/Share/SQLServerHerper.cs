using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF项目.Share
{
    /// <summary>
    /// 数据库访问入口（单例）
    /// 
    /// 说明：
    /// 1. 使用 Lazy&lt;T&gt; 实现延迟加载、线程安全的单例。
    /// 2. 只有第一次访问 Db.Instance 时，才会真正创建 SqlSugarClient。
    /// 3. SqlSugarClient 本身不是线程安全的；如果在多线程、Web、WPF 后台并发场景使用，
    ///    建议改用 SqlSugarScope。
    /// </summary>
    internal class SQLServerHerper
    {
        /// <summary>
        /// Lazy&lt;T&gt; 默认是线程安全的。
        /// 这里把“创建 SqlSugarClient”的逻辑包起来，实现延迟初始化。
        /// 也就是说：不访问 Instance，就不会创建数据库客户端。
        /// </summary>
        private readonly static object obj=new object();

        private static readonly Lazy<SqlSugarClient> _lazy = new Lazy<SqlSugarClient>(() => new SqlSugarClient(new ConnectionConfig()
        {
            // 从 App.config / Web.config 的 connectionStrings 节点中
            // 读取名为 "Conn" 的连接字符串。
            // 注意：如果配置文件中没有 "Conn"，这里会抛 NullReferenceException。
            ConnectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString,
            DbType = DbType.SqlServer,
            // 自动关闭连接：
            // true 表示每次执行完 SQL 后自动关闭连接，避免连接长期占用。
            // 默认是 false。
            IsAutoCloseConnection = true
        }));
        /// <summary>
        /// 全局唯一的 SqlSugarClient 实例。
        /// 
        /// 第一次访问时，会触发 _lazy.Value，执行上面的工厂方法创建对象；
        /// 之后再次访问，都会返回同一个 SqlSugarClient 实例。
        /// </summary>
        public static  SqlSugarClient InstanceSqlSugar => _lazy.Value;
        public static SqlSugarClient Connetion()
        {
            SqlSugarClient sqlSugar = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString,
                DbType=DbType.SqlServer,
                //自动关闭连接，默认false
                IsAutoCloseConnection = true
            });
            return sqlSugar;
           
        }
    }
}
