using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace _07_linq与EF框架.Context
{
    internal class linqDbModel: DbContext
    {
        public linqDbModel() : base("name=ConnectionString")
        {

        }
        public virtual DbSet<UserInfo> UserInfos { get; set; }

        public virtual DbSet<StudentInfo> StudentInfos { get; set; }
        public virtual DbSet<ClassRoom> ClassRooms { get; set; }
    }
}
