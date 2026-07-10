
using CustomerDAL;
using Model;
using System;
using System.Collections.Generic;

namespace 示例
{

    public class CustomerDAL : IBase<Customer>
    {


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Add(Customer user, bool isRegister)
        {


            foreach (Customer item in list)
            {
                bool flag = item.Id == user.Id;
                if (flag)
                {

                    // 添加日志
                    string Message = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},添加客户信息:{user.Name} 失败";
                    logDAL.LogTrade(new Log(user.Id, userDAL.GerUser(UserDAL.userId).Name, user.Name, DateTime.Now, Message));
                    return false;
                }
            }
            //为true 是添加
            if (isRegister)
            {
                // 添加日志
                string Message1 = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},添加客户信息:{user.Name} 成功";
                logDAL.LogTrade(new Log(user.Id, userDAL.GerUser(UserDAL.userId).Name, user.Name, DateTime.Now, Message1));
            }


            list.Add(user);

            return true;
        }

        // Token: 0x06000012 RID: 18 RVA: 0x000021D0 File Offset: 0x000003D0
        public bool Delete(int id)
        {

            foreach (Customer item in list)
            {
                bool flag = item.Id == id;
                if (flag)
                {
                    list.Remove(item);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GerUser(int id)
        {
            foreach (Customer item in list)
            {
                bool flag = item.Id == id;
                if (flag)
                {
                    //log
                    string Message = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},查询客户信息:{item.Name} 成功";
                    logDAL.LogTrade(new Log(id, userDAL.GerUser(UserDAL.userId).Name, item.Name, DateTime.Now, Message));
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update(Customer user)
        {
            foreach (Customer item in list)
            {
                bool flag = item.Id == user.Id;
                if (flag)
                {
                    string Message = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},修改客户信息:{item.Name} 成功";
                    logDAL.LogTrade(new Log(user.Id, userDAL.GerUser(UserDAL.userId).Name, item.Name, DateTime.Now, Message));
                    item.Name = user.Name;
                    item.Phone = user.Phone;
                    item.Balance = user.Balance;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 查询客户地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Address GetAddress(int id)
        {

            foreach (Address item in Addresses)
            {
                bool flag = item.Id == id;
                if (flag)
                {
                    string Message = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},查询客户地址:{item.Add} 成功";
                    logDAL.LogTrade(new Log(id, userDAL.GerUser(UserDAL.userId).Name, item.Add, DateTime.Now, Message));
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 添加客户地址
        /// </summary>
        /// <param name="address"></param>
        public bool AddAddress(Address address)
        {
            string Message = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},添加客户地址:{address.Add} 成功";
            logDAL.LogTrade(new Log(address.Id, userDAL.GerUser(UserDAL.userId).Name, address.Add, DateTime.Now, Message));
            Addresses.Add(address);
            return true;
        }
        /// <summary>
        /// 显示所有客户信息
        /// </summary>
        /// <returns></returns>
        public List<Address> ShowAddress()
        {
            string Message = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},查询所有客户信息成功";
            logDAL.LogTrade(new Log(0, userDAL.GerUser(UserDAL.userId).Name, "", DateTime.Now, Message));
            return Addresses;
        }

        /// <summary>
        /// 删除客户地址根据id
        /// </summary>
        /// <returns></returns>
        public bool DeleteAddress(int id)
        {
            foreach (Address item in Addresses)
            {
                bool flag = item.Id == id;
                if (flag)
                {
                    string Message = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},删除客户地址:{item.Add} 列表成功";
                    logDAL.LogTrade(new Log(id, userDAL.GerUser(UserDAL.userId).Name, item.Add, DateTime.Now, Message));
                    Addresses.Remove(item);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 修改客户地址
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool UpdateAddress(Address address)
        {
            foreach (Address item in Addresses)
            {
                bool flag = item.Id == address.Id;
                if (flag)
                {
                    string Message = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},修改客户地址:{item.Add} 列表成功";
                    logDAL.LogTrade(new Log(address.Id, userDAL.GerUser(UserDAL.userId).Name, item.Add, DateTime.Now, Message));
                    Addresses[address.Id]=address;
                    return true;
                }
            }
            return false;
        }

        // Token: 0x06000017 RID: 23 RVA: 0x000023DC File Offset: 0x000005DC
        public List<Customer> Show()
        {
            string Message = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},查询所有客户信息成功";
            logDAL.LogTrade(new Log(0, userDAL.GerUser(UserDAL.userId).Name, "", DateTime.Now, Message));
            return list;
        }

        public bool Register(Customer user)
        {
            return Add(user, false);
        }
        public List<Customer> GetName(string name)
        {
            string Message = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},查询客户信息:{name} 列表成功";
            logDAL.LogTrade(new Log(0, userDAL.GerUser(UserDAL.userId).Name, name, DateTime.Now, Message));
            return list.FindAll(x => x.Name.Contains(name));
        }

        // 客户列表
        public static List<Customer> list = new List<Customer>();

        // 客户地址列表
        public static List<Address> Addresses = new List<Address>();
        LogDAL logDAL = new LogDAL();
        UserDAL userDAL = new UserDAL();
    }
}
