using DAL;
using DALFactory;
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

            List<Address> list = new List<Address>();
            list.Find(x => x == address);
        }

        //CustomerDAL dal = new CustomerDAL();

        private readonly ICustomer dal = DataAccess.CreateDAL<ICustomer>("CustomerDAL");

        private readonly IUser user = DataAccess.CreateDAL<IUser>("UserDAL");
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
        public bool Updatedal(Customer customer)
        {
            if (user.IsGrade(UserDAL.userId).Grade != "管理员" && UserDAL.flag)
            {

                return false;
            }

            if (customer != null && dal.Update(customer))
            {
                return true;
            }
            else
            {
                return false;
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
        public List<Customer> GetAlldal(string where, int page, out int totalPage, int pageSize = 10, int Id = -1, string Name = "", string orderBy = "Id desc")
        {
           
            return dal.Show(where, page, pageSize, orderBy, out totalPage, Id, Name);
        }
       

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
        public List<Address> ShowAddress(string where, int page, out int totalPage, int pageSize = 10, int Id = -1, string Name = "", string orderBy = "Id desc")
        {
            

            return dal.ShowAddress(where, page, out totalPage, pageSize, orderBy, Name, Id);


        }
        /// <summary>
        /// 删除客户地址
        /// </summary>
        public bool DeleteAddress(int id)
        {
            if (user.IsGrade(UserDAL.userId).Grade != "管理员")
            {               
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
            if (address != null && dal.UpdateAddress(address))
            {

                return true;
            }
            return false;
        }


       
        

    }
}
