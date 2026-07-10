using System;

namespace 示例
{
    // Token: 0x02000009 RID: 9
    public class User
    {

        public int Id { get; set; }


        public string Name { get; set; }

        public int Age { get; set; }
        public char Sex { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsGraduate{ get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string describe { get; set; }
        public int Account { get; set; }
        public int Password { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? state { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string Grade { get; set; }


        public User()
        {
            this.Id = 0;
            this.Name = "";
            this.Account = 0;
            this.Password = 0;
            this.Grade = "";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">姓名</param>
        /// <param name="account">账户 int</param>
        /// <param name="password">密码 int</param>
        /// <param name="greade">权限</param>
        /// <param name="state">状态 为null状态正常 1删除</param>
        public User(int id, string name, int account, int password, string greade,int? state)
        {
            this.Id = id;
            this.Name = name;
            this.Account = account;
            this.Password = password;
            this.Grade = greade;
            this.state = state;
        }
        public User(int id, string name, int account, int password, string greade,string email,string Address,string describe)
        {
            this.Id = id;
            this.Name = name;
            this.Account = account;
            this.Password = password;
            this.Grade = greade;
            this.Email = email;
            this.Address = Address;
            this.describe = describe;
        }
        public override string ToString()
        {

            string mask = new string('*', Password.ToString().Length);
          
            Console.WriteLine("编号：{0}，姓名：{1}，账号：{2}，密码：{3}，等级：{4}", this.Id, this.Name, this.Account, mask, this.Grade);
            return null;

        }
        public User(int id, string name, int age, char sex, string phone, DateTime birth,bool isGraduate,string email,string Address,string describe)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Sex = sex;
            this.Phone = phone;
            this.Birthday = birth;
            this.IsGraduate = isGraduate;
            this.Email = email;
            this.Address = Address;
            this.describe = describe;
         
        }
        public User(int id,string name, int age, char sex, string phone, DateTime birth, int account, int password, bool isGraduate,string greade,string email,string Address,string describe)
        { 
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Sex = sex;
            this.Phone = phone;
            this.Birthday = birth;
            this.IsGraduate = isGraduate;
            this.Email = email;
            this.Account = account;
            this.Password = password;
            this.Grade = greade;
            this.Address = Address;
            this.describe = describe;
        }

    }
}
