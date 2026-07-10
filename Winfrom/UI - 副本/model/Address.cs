using System;
using System.Collections.Generic;
using System.Data;
using 示例;

namespace 示例
{
	// Token: 0x02000002 RID: 2
	public class Address
	{
		
		public int Id { get; set; }

		
		public string Add { get; set; }
        public int?  state { get; set; }


        public Address()
		{

		}

	
		public Address(int id, string add,int? state)
		{
			this.Id = id;
			this.Add = add;
            this.state = state;
		}
	}
}
