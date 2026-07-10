using System;
using System.Runtime.CompilerServices;

namespace 示例
{
	// Token: 0x02000003 RID: 3
	public class Customer
	{
		/// <summary>
		/// id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 手机号
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// 账户余额
		/// </summary>
		public double Balance { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? state { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Customer()
		{
			this.Id = 0;
			this.Name = "";
			this.Phone = "";
			this.Balance = 0.0;
		}

		
		public Customer(int id, string name, string phone, double balance, int? state)
		{
			this.Id = id;
			this.Name = name;
			this.Phone = phone;
			this.Balance = balance;
            this.state = state;
		}
		public override string ToString() {
			string blance = new string('*', Balance.ToString().Length);
           
            Console.WriteLine("用户id：{0} 用户名：{1} 手机号：{2} 余额：{3}", Id, Name, new string('*', Phone.Length - 4)+Phone.Substring(6, 4)  , blance);
			return null;

        }
	}
}
