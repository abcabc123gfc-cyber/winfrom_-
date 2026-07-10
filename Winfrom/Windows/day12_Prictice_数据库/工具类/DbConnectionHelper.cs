using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
            string str = ConfigurationManager.ConnectionStrings["connString2"].ConnectionString;
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
            catch (System.Exception)
            {

                return -1;
            }
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataSet GetReader(SqlConnection conn, string cmdSql, SqlParameter[] cmdParms)
        {


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

                return ex.Message;
            }
        }



    }
}
