using System;
using System.Collections.Generic;

namespace 示例
{
	
	public interface ICustomer : IBase<Customer>
	{
		/// <summary>
		/// 分页显示
		/// </summary>
		/// <param name="where"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <param name="orderBy"></param>
		/// <param name="totalPage"></param>
		/// <param name="Id"></param>
		/// <param name="Name"></param>
		/// <returns></returns>
        List<Customer> Show(string where, int page, int pageSize, string orderBy, out int totalPage, int Id, string Name);

		/// <summary>
		/// 获取地址
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		 Address GetAddress(int id);

		/// <summary>
		/// 添加地址
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		bool AddAddress(Address address);

		/// <summary>
		/// 分页显示地址
		/// </summary>
		/// <param name="where"></param>
		/// <param name="page"></param>
		/// <param name="totalPage"></param>
		/// <param name="pageSize"></param>
		/// <param name="orderBy"></param>
		/// <param name="Name"></param>
		/// <param name="Id"></param>
		/// <returns></returns>
		List<Address> ShowAddress(string where, int page, out int totalPage, int pageSize, string orderBy, string Name, int Id);

		/// <summary>
		/// 删除地址
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		bool DeleteAddress(int id);

        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="address"></param>
        bool UpdateAddress(Address address);
    }
}
