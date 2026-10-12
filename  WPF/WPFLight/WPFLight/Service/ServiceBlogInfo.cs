using System;
using System.Collections.Generic;
using System.Linq;
using WPFLight.Model;
using WPFLight.Share;

namespace WPFLight.Service
{
    internal class ServiceBlogInfo : Iservice<BlogInfo>
    {
        private readonly Func<SQLHelper> _dbFactory;
        // Service 构造函数接收工厂
        public ServiceBlogInfo(Func<SQLHelper> dbFactory)
        {
            _dbFactory = dbFactory;
            Console.WriteLine($"注入的 DbContext 哈希: {_dbFactory.GetHashCode()}");
            _dbFactory = dbFactory;
        }
        public List<BlogInfo> GetAll()
        {
            using (var db = _dbFactory())     // 用完即弃，最干净
            {
                return db.BlogInfos.ToList();
            }
        }
    }
}
