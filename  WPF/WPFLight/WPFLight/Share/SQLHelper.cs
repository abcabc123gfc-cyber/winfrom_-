using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using WPFLight.Model;
using System.Windows;
namespace WPFLight.Share
{
    internal class SQLHelper:DbContext
    {
        private readonly static string connectionString= ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public SQLHelper():base(connectionString)
        {
     

        }
        public DbSet<UserInfo> UserInfos { get; set;}
        public DbSet<BlogInfo> BlogInfos{ get; set;}
        
    }
}
