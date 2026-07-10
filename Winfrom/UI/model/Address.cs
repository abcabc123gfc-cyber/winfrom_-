using System;

namespace 示例
{
	// Token: 0x02000002 RID: 2
	public class Address
	{
		
		public int Id { get; set; }

		
		public string Add { get; set; }

		
		public Address()
		{

		}

	
		public Address(int id, string add)
		{
			this.Id = id;
			this.Add = add;
		}
	}
}
