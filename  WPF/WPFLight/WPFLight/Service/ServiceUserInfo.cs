using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLight.Model;
using WPFLight.Share;

namespace WPFLight.Service
{
    internal class ServiceUserInfo : Iservice<UserInfo>
    {
        //private readonly SQLHelper db;
        //public ServiceUserInfo(SQLHelper sQLHelper)
        //{
        //    db=sQLHelper;
        //}
        //public List<UserInfo> GetAll()
        //{

        //    return db.UserInfos.ToList();

        //}
        private readonly Func<SQLHelper> _dbFactory;

        public ServiceUserInfo(Func<SQLHelper> dbFactory)      // ← 收 Func
        {
            _dbFactory = dbFactory;
        }

        public List<UserInfo> GetAll()
        {
            using (var db = _dbFactory())                      // ← 每次新 DbContext
            {
                return db.UserInfos.ToList();
            }                                                  // ← 自动 Dispose
        }
    }
}
