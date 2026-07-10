using System;
using System.Collections;
using System.Collections.Generic;

namespace 示例
{
    public class CustomerBLL
    {
        public void test()
        {
            ArrayList arrayList = new ArrayList();
            Address address = arrayList[0] as Address;
            //没有返回值调用 default(T) 返回对应参数的默认值
            List<Address> list = new List<Address>();
            list.Find(x => x == address);
        }

        CustomerDAL dal = new CustomerDAL();
        UserDAL user = new UserDAL();
        /// <summary>
        /// 添加客户
        /// </summary>
        public bool Adddal(Customer customer)
        {
            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag)
            {

                return false;
            }

            if (customer != null && dal.Add(customer, true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改客户信息
        /// </summary>
        public void Updatedal()
        {
            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag)
            {
                Console.WriteLine("权限不足或未登录");
                return;
            }
            Customer customer = dalInfo();
            if (customer != null && dal.Update(customer))
            {
                Console.WriteLine("更新成功");
            }
            else
            {
                Console.WriteLine("更新失败未登录或用户不存在");
            }
        }

        /// <summary>
        /// 删除客户
        /// </summary>
        public bool Deletedal(int id)
        {
            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag)
            {

                return false;
            }

            if (dal.Delete(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据用户id查询客户信息
        /// </summary>
        public Customer Getdal(int id)
        {
            if (!UserDAL.flag)
            {
                Console.WriteLine("未登录");
                return null;
            }



            return dal.GerUser(id);


        }

        /// <summary>
        /// 显示所有客户信息
        /// </summary>
        public List<Customer> GetAlldal()
        {
            if (!UserDAL.flag)
            {
                Console.WriteLine("未登录");
                return null;
            }
            return dal.Show();
        }
        /// <summary>
        /// 根据用户名查询用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Customer> GetName(string name)
        {
            if (!UserDAL.flag)
            {
                Console.WriteLine("未登录");
                return null;
            }

            List<Customer> list = dal.GetName(name);
            if (list.Count > 0)
            {
                return list;
            }
            return null;

        }
        #region 客户地址信息
        /// <summary>
        /// 添加客户地址
        /// </summary>
        public bool AddAddress(Address address)
        {
            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag)
            {
               
                return false;
            }

            if (address != null)
            {
                dal.AddAddress(address);

                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据客户id查询用户地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Address GetAddress(int id)
        {
            return dal.GetAddress(id);

        }
        /// <summary>
        /// 显示所有用户地址
        /// </summary>
        /// <returns></returns>
        public List<Address> ShowAddress()
        {
            if (dal.ShowAddress().Count > 0)
            {
                return dal.ShowAddress();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 删除客户地址
        /// </summary>
        public bool DeleteAddress(int id)
        {
            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag)
            {
                Console.WriteLine("权限不足或未登录");
                return false;
            }

            if (dal.DeleteAddress(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改客户地址
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool UpdateAddress(Address address)
        {
            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag)
            {
                
                return false;
            }
            if (address != null&& dal.UpdateAddress(address))
            {
                
                return true;
            }
            return false;
        }
        #endregion
        #region 用户信息导入导出
        public Customer dalInfo()
        {
            try
            {

                Console.WriteLine("输入用户id");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("请输入用户名");
                string dalname = Console.ReadLine();
                Console.WriteLine("输入手机号");
                string account = Console.ReadLine();
                Console.WriteLine("输入余额");
                int password = int.Parse(Console.ReadLine());
                return new Customer(id, dalname, account, password);
            }
            catch (Exception)
            {

                Console.WriteLine("客户信息输入错误");
            }
            return null;
        }
        /// <summary>
        /// 设置客户地址
        /// </summary>
        /// <returns></returns>
        public Address SetCustomer()
        {
            try
            {

                Console.WriteLine("输入客户id");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("请输入用户地址");
                string dalname = Console.ReadLine();
                return new Address(id, dalname);
            }
            catch (Exception)
            {

                Console.WriteLine("客户地址输入错误");
            }
            return null;
        }
        #endregion
    }
}
