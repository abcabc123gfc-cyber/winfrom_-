using Microsoft.Data.SqlClient;
using System.Data;
using WebApi_ADO.Net.Model;

using WebApi项目使用ADO.NET.Helpers;

namespace WebApi_ADO.Net.Services
{
    public class UserInfoServices: IUserInfo
    {
        public bool Add(UserInfo userInfo)
        {
            string sql = "insert  into UserInfo(Name,Account,Password,Grade,State) values(@Name,@Account,@Password,@Grade,@State)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name",userInfo.Name),
                new SqlParameter("@Account",userInfo.Account),
                new SqlParameter("@Password",userInfo.Password),
                new SqlParameter("@Grade",userInfo.Grade),
                new SqlParameter("@State",userInfo.State)
            };
           int row= SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString,CommandType.Text, sql, parameters);
            return row>0;
        }

       

        public bool Remove(int id)
        {
            return default;
        }
    }
}
