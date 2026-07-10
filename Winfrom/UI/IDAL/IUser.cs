using System;

namespace 示例
{
	// Token: 0x02000007 RID: 7
	public interface IUser : IBase<User>
	{
		// Token: 0x0600001D RID: 29
		bool Login(int acound, int password);

		
		User IsGrade(int id);
		/// <summary>
		/// 数据备份
		/// </summary>
		/// <returns></returns>
		bool DataBackup();
		/// <summary>
		/// 个人信息
		/// </summary>
		/// <returns></returns>
		User Personalinfo();


    }
}
