
using day12_Prictice_数据库.工具类;
using model;
using Model;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using 示例;

namespace DAL
{
    // Token: 0x0200000A RID: 10
    public class UserDAL : IUser
    {

        public UserDAL()
        {

        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="flag1">lag1为true 时添加用户，记录日志</param>
        /// <returns></returns>
        public bool Add(User user, bool flag1)
        {
            if (flag1)
            {

                string messageTemplate = $"添加用户{user.Name}";
                logDAL.LogOperrate(new Logs(userId, LoggedInUser.user.Name, DateTime.Now, null, messageTemplate));

            }



            SqlParameter[] sqlParameters = GetSqlParameters(user);
            int num = DbConnectionHelper.GetCommand(DbConnectionHelper.GetConnection(), "INSERT INTO UserInfo VALUES(@Name,@Account,@Password,@Grade,@State)", sqlParameters);
            if (num > 0)

            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            if (id == LoggedInUser.user.Id) return false;
            string messageTemplate = $"删除用户Id: {id}";
            logDAL.LogOperrate(new Logs(LoggedInUser.user.Id, LoggedInUser.user.Name, DateTime.Now, null, messageTemplate));
            string where = "UPDATE UserInfo SET State=@State WHERE Id=@Id";
            //"DELETE FROM UserInfo WHERE Id=@Id"
            SqlParameter[] sqlParameters = new SqlParameter[]
                        {
                            new SqlParameter("@State", 1),
                            new SqlParameter("@Id", id)
                        };
            int num = DbConnectionHelper.GetCommand(DbConnectionHelper.GetConnection(), where, sqlParameters);
            if (num > 0)
            {
                return true;
            }
            return false;
        }

        //根据名字查询
        public List<User> GetName(string name)
        {
            List<User> list = new List<User>();
            foreach (User item in UserDAL.listUser)
            {
                bool flag = item.Name.Contains(name);
                if (flag)
                {

                    list.Add(item);
                }
            }
            return list;
        }



        // Token: 0x0600002E RID: 46 RVA: 0x00002878 File Offset: 0x00000A78
        public User GerUser(int id)
        {
            if (!flag)
            {

                return null;
            }
            try
            {
                return LoggedInUser.user;

            }
            catch (Exception ex)
            {
                logDAL.LogOperateException("UserDAL", "查询用户", ex);
                return null;
            }


        }


        public List<User> GetUserName(string name)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Name", name)
            };
            return default;
        }

        #region 登录
        public bool Login(int account, int password)
        {

            try
            {

                string sql = @"SELECT  * FROM UserInfo WHERE Account = @Account AND Password = @Password";

                SqlParameter[] pms = {
            new SqlParameter("@Account", account),
            new SqlParameter("@Password", password) // 后续应改为哈希
        };


                DataTable dataTable = DbConnectionHelper.GetExecuteDataTable(DbConnectionHelper.GetConnection(), sql, pms);

                if (dataTable == null || dataTable.Rows.Count == 0) return false;

                #region 解析
                DataRow row = dataTable.Rows[0];

                User u = new User(
                    (int)row["Id"],
                    row["Name"].ToString(),
                    (int)row["Account"],
                    (int)row["Password"],
                    (int)row["Grade"] == 0 ? "管理员" : "操作员",
                    (int)row["state"]  // 直接转换，因为不再是 NULL
                );
                #endregion

                LoggedInUser.user = u;
                flag = true;

                userId = LoggedInUser.user.Id;

                return true;
            }
            catch (Exception)
            {

                throw;
            }


        }


        #endregion
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update(User user)
        {
            try
            {

                string messageTemplate = $"修改用户信息:{user.Id}";
                logDAL.LogOperrate(new Logs(userId, LoggedInUser.user.Name, DateTime.Now, null, messageTemplate));

                int num = DbConnectionHelper.GetCommand(DbConnectionHelper.GetConnection(), "UPDATE UserInfo SET Name=@Name,Account=@Account,Password=@Password,Grade=@Grade,State=@State WHERE Id=@Id", GetSqlParameters(user));
                if (num > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="page">当前页码</param>
        /// <param name="pageSize">每页显示条数，默认值：10</param>
        /// <param name="orderBy">排序列，默认值：创建时间倒序</param>
        /// <returns></returns>
        public List<User> Show(string where, int page, out int totalPage, int pageSize, string orderBy, string Name, int Id)
        {
            if (!flag)
            {
                totalPage = 0;
                return null;
            }
            try
            {

                string messageTemplate = $"用户{LoggedInUser.user.Name}查看所有用户信息";
                logDAL.LogOperrate(new Logs(userId, LoggedInUser.user.Name, DateTime.Now, null, messageTemplate));
                // 获取数据集
                DataTable dataTable = GetDataSet(where, page, pageSize, orderBy, out totalPage, Id, Name);
                if (dataTable == null || dataTable.Rows.Count == 0) return null;
                // 数据解析
                List<User> listUser = GetDataTable(dataTable);

                return listUser;
            }
            catch (Exception ex)
            {
                logDAL.LogOperateException("UserDAL", "返回User列表", ex);
                totalPage = 0;
                return null;
            }
        }
        #region 数据解析
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
                string sql = @"SELECT * FROM UserInfo WHERE State=@State ";
                string sql2 = @"SELECT COUNT(1) FROM UserInfo WHERE State=@State  ";

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
                totalPage = GetTotalPage(sql2, pageSize, sqlParameters);
                return dataTable;
            }
            catch (Exception ex)
            {
                logDAL.LogOperateException("UserDAL", "返回 dataTable表格", ex);
                totalPage = 0;
                return null;
            }


        }
        /// <summary>
        /// 将表格解析成List<User>
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private List<User> GetDataTable(DataTable dataTable)
        {
            try
            {

                List<User> listUser = new List<User>();
                foreach (DataRow row in dataTable.Rows)
                {
                    listUser.Add(new User(
                                        (int)row["Id"],
                                        row["Name"].ToString(),
                                        (int)row["Account"],
                                        (int)row["Password"],
                                        (int)row["Grade"] == 0 ? "管理员" : "操作员",
                                        (int)row["state"]  // 直接转换，因为不再是 NULL
                                    ));

                }
                return listUser;
            }
            catch (Exception ex)
            {
                logDAL.LogOperateException("UserDAL", "返回 List<User> 列表", ex);
                return null;
            }

        }
        /// <summary>
        /// 获取SqlParameter[]
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private SqlParameter[] GetSqlParameters(User user)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
          {
                new SqlParameter("@Id", user.Id),
                new SqlParameter("@Name", user.Name),
                new SqlParameter("@Account", user.Account),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Grade", user.Grade=="管理员"?0:1),
                new SqlParameter("@State", user.state)
          };
            return sqlParameters;
        }
        #endregion
        #region 总页数查询
        private int GetTotalPage(string where, int pageSize, SqlParameter[] sqlParameters)
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
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Register(User user)
        {
            if (Add(user, false))
            {
                string messageTemplate = $"用户{user.Name}注册成功";

                logDAL.LogOperrate(new Logs(user.Id, user.Name, DateTime.Now, null, messageTemplate));
                return true;
            }
            else
            {
                return false;

            }

        }


        public User IsGrade(int id)
        {
            return GerUser(id);

        }

        #region 数据备份
        public bool DataBackup()
        {
            try
            {


                string sql = @"SELECT * FROM UserInfo ;  SELECT * FROM CustomerInfo; SELECT * FROM AddressInfo;";
                string[] str = new string[]
                {
                    "UserInfo.json",
                    "CustomerInfo.json",
                    "AddressInfo.json",
                };

                CustomerDAL customerDAL = new CustomerDAL();
                DataSet dataSet = GetDataSet(sql);
                if (dataSet == null) return false;


                if (!SaveToJson(GetDataTable(dataSet.Tables[0]), str[0])) return false;
                if (!SaveToJson(customerDAL.GetDataTable(dataSet.Tables[1]), str[1])) return false;
                if (!SaveToJson(customerDAL.GetAddressDataTable(dataSet.Tables[2]), str[2])) return false;
                string messageTemplate = $"数据备份成功";
                logDAL.LogOperrate(new Logs(LoggedInUser.user.Id, LoggedInUser.user.Name, DateTime.Now, null, messageTemplate));
                return true;
            }
            catch (Exception ex)
            {
                logDAL.LogOperateException("UserDAL", "数据备份出错", ex);
                return false;
            }

        }
        /// <summary>
        /// 获取dataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataSet GetDataSet(string sql)
        {

            return DbConnectionHelper.GetReader(DbConnectionHelper.GetConnection(), sql, null);
        }

        /// <summary>
        /// 获取应用程序的 Debug 目录（即 exe 所在目录）
        /// </summary>
        private static string GetDebugDirectory()
        {
            // 获取当前执行程序集的目录（bin\Debug\...）
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 将对象序列化为 JSON 并保存到 Debug 目录下的指定文件名
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要保存的对象</param>
        /// <param name="fileName">文件名（不含路径），如 "logData.json"</param>
        /// <param name="indented">是否格式化缩进，默认为 true 便于阅读</param>
        /// <returns>保存是否成功</returns>
        public static bool SaveToJson<T>(T obj, string fileName, bool indented = true)
        {
            try
            {
                if (obj == null) return false;
                // 拼接完整路径
                string fullPath = Path.Combine(GetDebugDirectory(), fileName);

                // 确保目录存在（如果文件名包含子目录，则一并创建）
                string dir = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // 序列化设置
                var settings = new JsonSerializerSettings
                {
                    Formatting = indented ? Formatting.Indented : Formatting.None,

                };

                string json = JsonConvert.SerializeObject(obj, settings);


                File.WriteAllText(fullPath, json, System.Text.Encoding.UTF8);

                //Console.WriteLine($"JSON 已保存至: {fullPath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"保存 JSON 失败: {ex.Message}");
                return false;
            }
        }
        #endregion
        public bool LogOut()
        {
            if (flag)
            {
                string messageTemplate = $"用户{LoggedInUser.user.Name}登出";
                logDAL.LogOperrate(new Logs(LoggedInUser.user.Id, LoggedInUser.user.Name, DateTime.Now, DateTime.Now, messageTemplate));
                UserDAL.flag = false;
                userId = -1;
                return true;
            }
            else
            {
                return false;
            }


        }


        public User Personalinfo()
        {
            try
            {

                return LoggedInUser.user;

            }
            catch (Exception)
            {


                return null;
            }
        }
        #region 导出 Excel
        public bool OutputExcel()
        {
            if (GetExcel("用户表")) return true;
            return false;
        }
        private bool GetExcel(string tableName)
        {
            string[] str = new string[]
               {
                    "UserInfo.xlsx",
                    "CustomerInfo.xlsx",
                    "AddressInfo.xlsx",
               };

            string sql = @"SELECT * FROM UserInfo ;  SELECT * FROM CustomerInfo; SELECT * FROM AddressInfo;";
            DataSet dataSet = GetDataSet(sql);
            if (dataSet == null) return false;
            CustomerDAL customerDAL = new CustomerDAL();
            for (int i = 0; i < str.Length; i++)
            {

                string filePath = Path.Combine(GetDirectoryExcel(), str[i]);

                FileInfo fileInfo = new FileInfo(filePath);
                //创建流
                using (ExcelPackage excel = new ExcelPackage(fileInfo))
                {
                    //删除工作表 ,如果存在
                    if (excel.Workbook.Worksheets[tableName] != null)
                    {

                        excel.Workbook.Worksheets.Delete(tableName);
                    }
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add(tableName);

                    //将list加载到工作表中, 参数2将list的属性值作为列名
                    if (GetDataTable(dataSet.Tables[i]) != null)
                    {
                        worksheet.Cells["A1"].LoadFromCollection(GetDataTable(dataSet.Tables[0]), true);

                    }
                    else if (customerDAL.GetAddressDataTable(dataSet.Tables[i]) != null)
                    {
                        worksheet.Cells["A1"].LoadFromCollection(customerDAL.GetAddressDataTable(dataSet.Tables[2]), true);
                    }
                    else if (customerDAL.GetDataTable(dataSet.Tables[i]) != null)
                    {
                        worksheet.Cells["A1"].LoadFromCollection(customerDAL.GetDataTable(dataSet.Tables[1]), true);
                    }
                    //保存
                    excel.Save();
                }
            }
            return true;
        }


        #region  返回表格存储路径
        private static string GetDirectoryExcel()
        {
            string str = AppDomain.CurrentDomain.BaseDirectory + "//Excel";
            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
            }
            return str;
        }
        #endregion

        #endregion



        #region 实例化日志
        LogDAL logDAL = new LogDAL();
        #endregion
        #region 静态变量
        // Token: 0x0400000E RID: 14
        public static List<User> listUser = new List<User>();

        /// <summary>
        /// 用户登录状态
        /// </summary>
        public static bool flag = false;
        public static int userId = -1;
        #endregion

    }
}
