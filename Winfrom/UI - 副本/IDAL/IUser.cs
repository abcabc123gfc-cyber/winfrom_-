using System;
using System.Collections.Generic;

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
		/// <summary>
		/// 显示
		/// </summary>
		/// <param name="where"></param>
		/// <param name="page"></param>
		/// <param name="totalPage"></param>
		/// <param name="pageSize"></param>
		/// <param name="orderBy"></param>
		/// <param name="Name"></param>
		/// <param name="Id"></param>
		/// <returns></returns>
		List<User> Show(string where, int page, out int totalPage, int pageSize, string orderBy, string Name, int Id);
		/// <summary>
		/// 登出
		/// </summary>
		/// <returns></returns>
		bool LogOut();

		/// <summary>
		/// 导出Excel
		/// </summary>
		/// <returns></returns>
		bool OutputExcel();
		
    }
}
