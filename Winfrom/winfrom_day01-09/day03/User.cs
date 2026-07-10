using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day03
{
    public  class User
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public string state { get; set;}
        public string Phone { get; set;}

        public List<User> users()
        {
            return new List<User>()
            {
                new User() { Id = 1, Name = "张三", state = "正常", Phone = "123456" },
            };
        }
    }
}
