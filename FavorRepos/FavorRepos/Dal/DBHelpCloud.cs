using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FavorRepos.Dal
{
    public class DBHelpCloud
    {

        /// <summary>
        /// 连接数据库
        /// </summary>
        private static readonly string conString = "Data Source=(local);Initial Catalog=MyFavorRepos;Persist Security Info=True;User ID=sa;Password=123456;Max Pool Size=1000";


        public static string ConString
        {
            get { return DBHelpCloud.conString; }
        }

        //得到一条记录
        public static string get_one_record(string sql)
        {
            string id = "";
            SqlConnection sqlcon = new SqlConnection(conString);
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlcon);
            object c = cmd.ExecuteScalar();
            if (c != null)
            {
                id = c.ToString();
            }
            else
            {
                id = "";
            }
            sqlcon.Close();
            return id;
        }


        /// <summary>
        /// 执行查询所有
        /// </summary>
        /// <param name="sql"></param>o
        /// <param name="commandType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReader(string sqlstr, CommandType commandType, params SqlParameter[] param)
        {
            SqlDataReader dataReader = null;
            using (SqlConnection sqlcon = new SqlConnection(conString))
            {
                using (SqlCommand sqlcom = new SqlCommand(sqlstr, sqlcon))
                {
                    sqlcom.CommandType = commandType;
                    if (param.Length != 0)
                    {
                        sqlcom.Parameters.AddRange(param);
                    }
                    try
                    {
                        sqlcon.Open();
                        dataReader = sqlcom.ExecuteReader();
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }
                }
            }
            return dataReader;
        }

        /// <summary>
        ///  执行增加、修改、删除功能
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExcuteCommand(string sqlstr, CommandType commandType, params SqlParameter[] param)
        {
            int flage = -1;
            using (SqlConnection sqlcon = new SqlConnection(conString))
            {
                using (SqlCommand sqlcom = new SqlCommand(sqlstr, sqlcon))
                {
                    sqlcom.CommandType = commandType;
                    if (param.Length != 0)
                    {
                        sqlcom.Parameters.AddRange(param);
                    }
                    try
                    {
                        sqlcon.Open();
                        flage = sqlcom.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }
                }
            }
            return flage;
        }

        /// <summary>
        /// 查询返回一条
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="param"></param>
        /// <returns></returns>       
        public static Object GetScalar(string sql, CommandType commandType, params SqlParameter[] param)
        {
            object flage = null;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = commandType;
                    if (param.Length != 0)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    try
                    {
                        con.Open();
                        flage = cmd.ExecuteScalar();
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }
                }
            }
            return flage;
        }

        private static SqlConnection connection;
        /// <summary>
        /// 构建数据库连接对象
        /// </summary>
        public static SqlConnection Connection
        {
            get
            {

                string connectionString = conString;
                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }

                return connection;

            }
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteCommand(string sql)
        {
            int result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        //返回操作影响的函数
                        result = cmd.ExecuteNonQuery();
                        //connection.Close();
                    }
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.ToString());
            }

            return result;
        }
        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="param"></param>
        /// <returns></returns>       
        public static DataSet GetDataSet(string sql, CommandType commandType, params SqlParameter[] param)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = sql;
                comm.CommandType = commandType;
                comm.Connection = con;
                if (param != null)
                {
                    if (param.Length != 0)
                    {
                        comm.Parameters.AddRange(param);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = comm;
                try
                {
                    con.Open();
                    da.Fill(ds, "table");
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    comm.Dispose();
                    comm = null;
                }
            }
            return ds;
        }
        public static DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = sql;
                comm.CommandType = CommandType.Text;
                comm.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = comm;
                try
                {
                    con.Open();
                    da.Fill(ds, "table");
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    comm.Dispose();
                    comm = null;
                }
            }
            return ds;
        }

        public static DataTable GetDataTable(string sql)
        {
            return GetDataSet(sql).Tables[0];
        }
        public static DataTable GetDataTable(string sql, CommandType commandType, params SqlParameter[] param)
        {
            return GetDataSet(sql, commandType, param).Tables[0];
        }
        //添加
        public static bool Insert_database(string sql)
        {
            bool insert_result = true;
            using (SqlConnection sqlcon = new SqlConnection(conString))
            {
                string sqlstr = sql;
                using (SqlCommand sqlcom = new SqlCommand(sqlstr, sqlcon))
                {
                    sqlcon.Open();
                    int count = sqlcom.ExecuteNonQuery();
                    if (count != 1)
                    {
                        insert_result = false;
                    }
                }
            }
            return insert_result;
        }
        //添加一条记录,并且返回主键值
        public static int Insert_databaseReturnPriKey(string sql)
        {
            int insert_result = -1;
            using (SqlConnection sqlcon = new SqlConnection(conString))
            {
                string sqlstr = sql;
                using (SqlCommand sqlcom = new SqlCommand(sqlstr, sqlcon))
                {
                    sqlcon.Open();
                    try
                    {
                        object c = sqlcom.ExecuteScalar();
                        if (c != null)
                        {
                            insert_result = int.Parse(c.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        insert_result = -1;
                    }
                }
            }
            return insert_result;
        }

        /// <summary>
        /// 查询是否存在,如果已有返回true
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>true表示存在</returns>
        public static bool Check_exists(string sql)
        {
            bool check_result = false;
            string sqlstr = sql;
            using (SqlConnection sqlcon = new SqlConnection(conString))
            {
                using (SqlDataAdapter myda = new SqlDataAdapter(sqlstr, sqlcon))
                {
                    DataSet myds = new DataSet();
                    sqlcon.Open();
                    int count = myda.Fill(myds, "check_project");
                    if (count != 0)
                    {
                        check_result = true;
                    }
                }
            }
            return check_result;
        }

        public static int Update_Table_Field_Command(string sql)
        {
            int result = -1;
            using (SqlConnection sqlcon = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, sqlcon))
                {
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        //得到一条记录
        public static string Get_one_record(string sql)
        {
            string id = "";

            using (SqlConnection sqlcon = new SqlConnection(conString))
            {
                sqlcon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlcon))
                {
                    object c = cmd.ExecuteScalar();
                    if (c != null)
                    {
                        id = c.ToString();
                    }
                    else
                    {
                        id = "";
                    }
                }
            }
            return id;
        }
    }
}
