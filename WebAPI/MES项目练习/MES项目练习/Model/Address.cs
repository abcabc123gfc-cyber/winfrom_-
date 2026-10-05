using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_练习.Model
{
	// Token: 0x02000002 RID: 2
	[Table("AddressInfo")]
	public class Address
	{
		[Column("Id"),Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Column("Address")]
		public string Add { get; set; }

		public int State { get; set; }
		
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
