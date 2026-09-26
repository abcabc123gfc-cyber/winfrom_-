using SqlSugar;

namespace WebApi_SqlSuger.Model
{

    [SugarTable("UserInfo")]
    public class UserInfo
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int UserId { get; set; }

        public string Account{ get; set; }

        public string Password { get; set; }
        public int CreateUserId { get; set; }

        public DateTime CreateTime { get; set;   }

        public int LastUpdateUserId { get; set; }

        public DateTime LastUpdateTime {  get; set; }

        public int Status { get; set; }


    }
}
