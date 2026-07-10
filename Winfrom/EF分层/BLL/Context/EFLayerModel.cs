using BLL.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Context
{
    public class EFLayerModel: DbContext
    {
        public EFLayerModel() : base("name=conneString")
        {


        }
        public DbSet<User> Users { get; set; }
    }
}
