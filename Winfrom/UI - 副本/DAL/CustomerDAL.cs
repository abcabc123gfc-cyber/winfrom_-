
using day12_Prictice_数据库.工具类;
using model;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using 示例;

namespace DAL
{

    public class CustomerDAL : ICustomer
    {


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Add(Customer user, bool isRegister)
        {




            try
            {


                string Message1 = $"添加客户信息:{user.Name} 成功";
                logDAL.LogTrade(new Logs(LoggedInUser.user.Id, "操作员:" + LoggedInUser.user.Name, user.Name, DateTime.Now, Message1));
                string sql = @"insert into CustomerInfo(Name,Phone,Blance,State) values(@Name,@Phone,@Blance,@State)";
                SqlParameter[] cmdParms = GetSqlParameters(user);
                int i = DbConnectionHelper.GetCommand(DbConnectionHelper.GetConnection(), sql, cmdParms);
                if (i > 0) return true;

                return false;

            }
            catch (Exception ex)
            {

                logDAL.LogOperateException("CustomerDAL", "添加客户信息", ex);

                return false;

            }

        }
        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                logDAL.LogOperrate(new Logs(id, model.LoggedInUser.user.Name, DateTime.Now, null, "删除客户信息"));
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                new SqlParameter("@Id",SqlDbType.Int),
                new SqlParameter("@State",SqlDbType.Int)
                };
                sqlParameters[0].Value = id;
                sqlParameters[1].Value = 1;
                //string sql = "delete from Customer where Id=@Id";
                string sql = "update CustomerInfo set State=@State where Id=@Id";
                int num = DbConnectionHelper.GetCommand(DbConnectionHelper.GetConnection(), sql, sqlParameters);
                if (num > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {

                logDAL.LogOperateException("CustomerDAL", "删除客户信息", ex);

                return false;

            }

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
                    logDAL.LogTrade(new Logs(id, userDAL.GerUser(UserDAL.userId).Name, item.Name, DateTime.Now, Message));
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



            try
            {

                string Message = $"修改客户信息:{user.Name} 成功";
                logDAL.LogTrade(new Logs(LoggedInUser.user.Id, " 操作员 " + LoggedInUser.user.Name, user.Name, DateTime.Now, Message));
                string sql = "update CustomerInfo set Name=@Name,Phone=@Phone, Blance = @Blance,State=@State  where Id=@Id";
                SqlParameter[] cmdParms = GetSqlParameters(user, user.Id);

                int num = DbConnectionHelper.GetCommand(DbConnectionHelper.GetConnection(), sql, cmdParms);
                return true;
            }
            catch (Exception ex)
            {

                logDAL.LogOperateException("CustomerDAL", "修改客户信息", ex);
                return false;
            }



        }



        /// <summary>
        /// 客户分页查询
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="totalPage"></param>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public List<Customer> Show(string where, int page, int pageSize, string orderBy, out int totalPage, int Id, string Name)
        {
            try
            {

                if (!UserDAL.flag)
                {
                    totalPage = 0;
                    return null;
                }

                string Message = $" 查询所有客户信息成功";
                logDAL.LogTrade(new Logs(LoggedInUser.user.Id, LoggedInUser.user.Name, "", DateTime.Now, Message));


                DataTable dataTable = GetDataSet(where, page, pageSize, orderBy, out totalPage, Id, Name);
                if (dataTable == null) return default;
                List<Customer> customers = GetDataTable(dataTable);


                return customers;
            }
            catch (Exception ex)
            {

                logDAL.LogOperateException("CustomerDAL", "返回客户列表", ex);
                totalPage = 0;
                return null;
            }

        }

        #region 客户数据解析

        private DataTable GetDataSet(string where, int page, int pageSize, string orderBy, out int totalPage, int Id, string Name)
        {

            try
            {

                SqlParameter[] sqlParameters = new SqlParameter[]
                   {
                    new SqlParameter("@page", SqlDbType.Int),
                    new SqlParameter("@pageSize", SqlDbType.Int),
                    new SqlParameter("@Id", Id),
                    new SqlParameter("@Name", Name),
                    new SqlParameter("@State", SqlDbType.Int )
                   };
                sqlParameters[0].Value = page;
                sqlParameters[1].Value = pageSize;
                sqlParameters[4].Value = 0;
                string sql = @"SELECT * FROM CustomerInfo WHERE State=@State ";
                string sql2 = @"SELECT COUNT(1) FROM CustomerInfo WHERE State=@State  ";

                if (!string.IsNullOrWhiteSpace(where))
                {
                    sql += where;
                    sql2 += where;
                }
                if (Id >= 1)
                {
                    sqlParameters[2].Value = Id;

                }
                else if (!string.IsNullOrWhiteSpace(Name))
                {
                    sqlParameters[3].Value = Name;
                }
                sql += " ORDER BY " + orderBy + " OFFSET (@page-1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY";

                DataTable dataTable = DbConnectionHelper.GetExecuteDataTable(DbConnectionHelper.GetConnection(), sql, sqlParameters);
                totalPage = GetCustomerTotalPage(sql2, pageSize, sqlParameters);
                return dataTable;
            }
            catch (Exception ex)
            {
                logDAL.LogOperateException("CustomerDAL", "返回 dataTable表格", ex);
                totalPage = 0;
                return null;
            }


        }
        /// <summary>
        /// 将表格解析成List<Customer>
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<Customer> GetDataTable(DataTable dataTable)
        {
            try
            {

                List<Customer> listCustomer = new List<Customer>();
                foreach (DataRow row in dataTable.Rows)
                {
                    listCustomer.Add(new Customer(
                                        (int)row["Id"],
                                        row["Name"].ToString(),
                                        row["Phone"].ToString(),
                                       (double)row["Blance"],
                                         (int)row["State"]
                                    ));

                }
                return listCustomer;
            }
            catch (Exception ex)
            {
                logDAL.LogOperateException("CustomerDAL", "返回 List<Customer> 列表", ex);
                return null;
            }

        }
        /// <summary>
        /// 获取SqlParameter[]
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private SqlParameter[] GetSqlParameters(Customer user, int Id = -1)
        {

            SqlParameter[] sqlParameters = new SqlParameter[]
          {

                new SqlParameter("@Id",SqlDbType.Int),
                new SqlParameter("@Name", user.Name),
                new SqlParameter("@Phone", user.Phone),
                new SqlParameter("@Blance", user.Balance),

                new SqlParameter("@State", SqlDbType.Int)
          };

            sqlParameters[4].Value = 0;
            if (user.Id > 0)
            {
                sqlParameters[0].Value = user.Id;
            }
            return sqlParameters;
        }
        #region 总页数查询
        private int GetCustomerTotalPage(string where, int pageSize, SqlParameter[] sqlParameters)
        {
            var cloned = sqlParameters.Select(p => new SqlParameter(p.ParameterName, p.Value)
            {
                SqlDbType = p.SqlDbType,
                Size = p.Size
            }).ToArray();
            int pages = (int)DbConnectionHelper.GetExecuteScalar(DbConnectionHelper.GetConnection(), where, cloned);
            double totalPage = Math.Ceiling(Convert.ToDouble(pages) / (double)pageSize);
            return (int)totalPage;
        }
        #endregion

        #endregion


        //-----------------------------------------------

        #region 地址
        /// <summary>
        /// 查询客户地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Address GetAddress(int id)
        {



            return default;



        }

        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="address"></param>
        public bool AddAddress(Address address)
        {
            string Message = $" 添加客户地址:{address.Add} 成功";
            logDAL.LogTrade(new Logs(LoggedInUser.user.Id, LoggedInUser.user.Name, address.Add, DateTime.Now, Message));
            SqlParameter[] sqlParameters = GetAddressSqlParameters(address);

            string sql = "insert into AddressInfo(Address,State) values(@Address,@State)";
            int num = DbConnectionHelper.GetCommand(DbConnectionHelper.GetConnection(), sql, sqlParameters);
            if (num > 0) return true;
            return false;
        }

        /// <summary>
        /// 显示所有客户地址信息
        /// </summary>
        /// <returns></returns>
        public List<Address> ShowAddress(string where, int page, out int totalPage, int pageSize, string orderBy, string Name, int Id)
        {
            try
            {

                string Message = $"查询所有客户信息成功";
                logDAL.LogTrade(new Logs(LoggedInUser.user.Id, LoggedInUser.user.Name, "", DateTime.Now, Message));
                DataTable dataTable = GetAddressDataSet(where, page, pageSize, orderBy, out totalPage, Id, Name);

                if (dataTable == null) return default;
                List<Address> addresses = GetAddressDataTable(dataTable);
                return addresses;
            }
            catch (Exception ex)
            {
                logDAL.LogOperateException("AddressBLL", "显示所有地址信息", ex);
                totalPage = 0;
                return null;

            }
        }

        /// <summary>
        /// 删除客户地址根据id
        /// </summary>
        /// <returns></returns>
        public bool DeleteAddress(int id)
        {
            try
            {

                SqlParameter[] cmdParms = GetAddressSqlParameters(null, id);
                //string sql=$"delete from Address where Id=@Id";
                string sql = $"update AddressInfo set State=@State where Id=@Id";
                int result = DbConnectionHelper.GetCommand(DbConnectionHelper.GetConnection(), sql, cmdParms);
                if (result > 0)
                {



                    string Message = $" 删除客户地址:{id} 列表成功";
                    logDAL.LogTrade(new Logs(LoggedInUser.user.Id, LoggedInUser.user.Name, DateTime.Now, null, Message));
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logDAL.LogOperateException("AddressBLL", "Delete", ex);
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
            foreach (Address item in Addresses)
            {
                bool flag = item.Id == address.Id;
                if (flag)
                {
                    string Message = $" 操作员: {userDAL.GerUser(UserDAL.userId).Name},修改客户地址:{item.Add} 列表成功";
                    logDAL.LogTrade(new Logs(address.Id, userDAL.GerUser(UserDAL.userId).Name, item.Add, DateTime.Now, Message));
                    Addresses[address.Id] = address;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 地址数据解析

        private DataTable GetAddressDataSet(string where, int page, int pageSize, string orderBy, out int totalPage, int Id, string Name)
        {

            try
            {

                SqlParameter[] sqlParameters = new SqlParameter[]
                   {
                    new SqlParameter("@page", SqlDbType.Int),
                    new SqlParameter("@pageSize", SqlDbType.Int),
                    new SqlParameter("@Id", Id),
                    new SqlParameter("@Address", Name),
                    new SqlParameter("@State", SqlDbType.Int )
                   };
                sqlParameters[0].Value = page;
                sqlParameters[1].Value = pageSize;
                sqlParameters[4].Value = 0;
                string sql = @"SELECT * FROM AddressInfo WHERE State=@State ";
                string sql2 = @"SELECT COUNT(1) FROM AddressInfo WHERE State=@State  ";

                if (!string.IsNullOrWhiteSpace(where))
                {
                    sql += where;
                    sql2 += where;
                }
                if (Id >= 1)
                {
                    sqlParameters[2].Value = Id;

                }
                else if (!string.IsNullOrWhiteSpace(Name))
                {
                    sqlParameters[3].Value = Name;
                }
                sql += " ORDER BY " + orderBy + " OFFSET (@page-1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY";

                DataTable dataTable = DbConnectionHelper.GetExecuteDataTable(DbConnectionHelper.GetConnection(), sql, sqlParameters);
                totalPage = GetAddressTotalPage(sql2, pageSize, sqlParameters);
                return dataTable;
            }
            catch (Exception ex)
            {
                logDAL.LogOperateException("CustomerDAL", "返回 dataTable表格", ex);
                totalPage = 0;
                return null;
            }


        }
        /// <summary>
        /// 将表格解析成List<Address>
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<Address> GetAddressDataTable(DataTable dataTable)
        {
            try
            {

                List<Address> listCustomer = new List<Address>();
                foreach (DataRow row in dataTable.Rows)
                {
                    listCustomer.Add(new Address(
                                (int)row["Id"],
                                row["Address"].ToString(),
                                  (int)row["State"]));

                }
                return listCustomer;
            }
            catch (Exception ex)
            {
                logDAL.LogOperateException("CustomerDAL", "返回 List<Address> 列表", ex);
                return null;
            }

        }
        /// <summary>
        /// 获取SqlParameter[]
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private SqlParameter[] GetAddressSqlParameters(Address user, int Id = -1, bool isUpdate = false)
        {

            SqlParameter[] sqlParameters = new SqlParameter[]
            {

                new SqlParameter("@Id",SqlDbType.Int),
                new SqlParameter("@Address", SqlDbType.VarChar),

                new SqlParameter("@State", SqlDbType.Int),
            };

            if (!isUpdate)
            {
                sqlParameters[2].Value = 1;

            }
            else
            {
                sqlParameters[2].Value = 0;

            }
            if (user != null)
            {
                sqlParameters[1].Value = user.Add;
            }
            if (user != null || Id > 0)
            {
                sqlParameters[0].Value = Id;
            }
            return sqlParameters;
        }
        #region 总页数查询
        private int GetAddressTotalPage(string where, int pageSize, SqlParameter[] sqlParameters)
        {
            var cloned = sqlParameters.Select(p => new SqlParameter(p.ParameterName, p.Value)
            {
                SqlDbType = p.SqlDbType,
                Size = p.Size
            }).ToArray();
            int pages = (int)DbConnectionHelper.GetExecuteScalar(DbConnectionHelper.GetConnection(), where, cloned);
            double totalPage = Math.Ceiling(Convert.ToDouble(pages) / (double)pageSize);
            return (int)totalPage;
        }
        #endregion

        #endregion


        [Obsolete("废弃方法")]
        public bool Register(Customer user)
        {
            return default;
        }
       


        public static List<Customer> list = new List<Customer>();
        public static List<Address> Addresses = new List<Address>();
        LogDAL logDAL = new LogDAL();
        UserDAL userDAL = new UserDAL();
    }
}
