using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml.Serialization;


namespace day12_Prictice_数据库.工具类
{
    public class DbConnectionHelper
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            string str = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            return new SqlConnection(str);
        }
        /// <summary>
        /// 执行SQL语句 增删改
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="cmdSql">执行的sql</param>
        /// <param name="cmdParms">参数注解</param>
        /// <returns></returns>
        public static int GetCommand(SqlConnection con, string cmdSql, SqlParameter[] cmdParms)
        {
            try
            {
                if (string.IsNullOrEmpty(cmdSql)) return -1;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                using (SqlConnection conn = con)
                using (SqlCommand cmd = new SqlCommand(cmdSql, conn))
                {


                    if (cmdParms != null && cmdParms.Length > 0)
                    {
                        // 可选的：将参数值为null的显式转为DBNull.Value（使意图更清晰）
                        foreach (var p in cmdParms)
                        {
                            if (p.Value == null)
                            {
                                p.Value = DBNull.Value;
                            }
                        }
                        cmd.Parameters.AddRange(cmdParms);
                    }

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Log("  执行SQL语句 增删改 GetCommand  ", ex);
                return -1;
            }
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns>数据集合</returns>
        public static DataSet GetReader(SqlConnection conn, string cmdSql, SqlParameter[] cmdParms)
        {
            try
            {
                if (string.IsNullOrEmpty(cmdSql)) return null;

                using (SqlCommand cmd = new SqlCommand(cmdSql, conn))
                {

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        if (cmdParms != null && cmdParms.Length > 0)
                        {
                            // 可选的：将参数值为null的显式转为DBNull.Value（使意图更清晰）
                            foreach (var p in cmdParms)
                            {
                                if (p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                            }
                            cmd.Parameters.AddRange(cmdParms);
                        }
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {

                            return ds;
                        }
                        else
                        {
                            return null;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Log("查询数据_数据集合 GetReader  ", ex);

                return null;
            }

        }
        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="con"> 连接对象</param>
        /// <param name="cmdSql">执行sql</param>
        /// <param name="cmdParms">参数注解</param>
        /// <returns>返回第一列结果</returns>
        public static object GetExecuteScalar(SqlConnection con, string cmdSql, SqlParameter[] cmdParms)
        {
            try
            {
                if (string.IsNullOrEmpty(cmdSql)) return null;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                using (SqlConnection conn = con)
                using (SqlCommand cmd = new SqlCommand(cmdSql, conn))
                {

                    if (cmdParms != null && cmdParms.Length > 0)
                    {


                        // 可选的：将参数值为null的显式转为DBNull.Value（使意图更清晰）
                        foreach (var p in cmdParms)
                        {
                            if (p.Value == null)
                            {
                                p.Value = DBNull.Value;
                            }
                        }
                        cmd.Parameters.AddRange(cmdParms);
                    }

                    return cmd.ExecuteScalar();
                }
            }
            catch (System.Exception ex)
            {
                Log("返回第一列结果 GetExecuteScalar", ex);
                return null;
            }
        }
        /// <summary>
        ///    获取数据表
        /// </summary>
        /// <param name="con"></param>
        /// <param name="cmdSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns>单张表返回 DataTable 类型</returns>
        public static DataTable GetExecuteDataTable(SqlConnection con, string cmdSql, SqlParameter[] cmdParms)
        {
            try
            {

                if (string.IsNullOrEmpty(cmdSql)) return null;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                using (SqlConnection conn = con)
                using (SqlCommand cmd = new SqlCommand(cmdSql, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {

                    if (cmdParms != null && cmdParms.Length > 0)
                    {
                        foreach (SqlParameter sqlParameter in cmdParms)
                        {
                            if (sqlParameter == null)
                            {
                                sqlParameter.Value = DBNull.Value;
                            }
                        }
                        cmd.Parameters.AddRange(cmdParms);
                    }
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        return dataTable;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                Log("GetExecuteDataTable 单张表返回 DataTable 类型", ex);
                throw;
            }
        }
        #region 日志
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="errorAction"> 执行语句</param>
        /// <param name="message">错误信息</param>
        private static void Log(string errorAction, Exception message)
        {
            try
            {
                string str = GetDebugDirectory();
                string fileDate = "log_" + "数据库操作_工具日志_" + ".xml";
                string pathFile = Path.Combine(str, fileDate);
                if (!File.Exists(pathFile))
                {
                    using (File.Create(pathFile)) { }
                }
                XmlSerializer serializer = new XmlSerializer(typeof(string));
                DateTime dateTime = DateTime.Now;
                string xml = $"执行语句: {errorAction}, 错误信息{message}" + dateTime.ToString();
                using (FileStream fs = new FileStream(pathFile, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    serializer.Serialize(sw, xml);
                    sw.WriteLine(); // 加个换行隔开多个对象
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region  返回日志存储地址
        private static string GetDebugDirectory()
        {
            string str = AppDomain.CurrentDomain.BaseDirectory + "//log";
            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
            }
            return str;
        }
        #endregion
        #endregion
    }
}






