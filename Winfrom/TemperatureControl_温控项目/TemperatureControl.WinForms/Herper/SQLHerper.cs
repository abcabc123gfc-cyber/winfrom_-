using SqlSugar;
using System.Configuration;

namespace TemperatureControl.WinForms.Herper
{
    public class SQLHerper
    {
        public static SqlSugarClient Connection()
        {
            SqlSugarClient sqlSugarClient = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString,
                DbType = DbType.SqlServer,
                //自动关闭连接，默认false
                IsAutoCloseConnection = true
            });
            return sqlSugarClient;
        }
    }
}
