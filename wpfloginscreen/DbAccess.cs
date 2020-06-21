using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace wpfloginscreen
{
   public static  class DBAccess
    {
        private static SqlConnection connection = new SqlConnection();
        private static SqlCommand command = new SqlCommand();
        private static SqlDataReader DbReader;
        private static SqlDataAdapter adapter = new SqlDataAdapter();
        public static SqlTransaction DbTran;

        private static string strConnString = "Data Source=(local);Initial Catalog=SocialNetwork;Integrated Security=True";


        public static string GetConnStr() 
        {

            return ConfigurationManager.ConnectionStrings["Zakazky"].ConnectionString;
        
        }

        public static string GetUserAlias()
        {

            return ConfigurationManager.ConnectionStrings["Alias"].ConnectionString;
        
        }

    
        public static void createConn()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = GetConnStr();
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Uzivatel VratUzivatele(string Jmeno)
        {
           using (IDbConnection konn = connection)
            {
                return konn.Query<Uzivatel>($"SELECT * FROM [Zakazky].[dbo].[ZADATEL] WHERE ALIAS= '{ Jmeno }'").Single();
            }


        }
        public static void closeConn()
        {
            connection.Close();
        }


        public static int executeDataAdapter(DataTable tblName, string strSelectSql)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }

                adapter.SelectCommand.CommandText = strSelectSql;
                adapter.SelectCommand.CommandType = CommandType.Text;
                SqlCommandBuilder DbCommandBuilder = new SqlCommandBuilder(adapter);


                string insert = DbCommandBuilder.GetInsertCommand().CommandText.ToString();
                string update = DbCommandBuilder.GetUpdateCommand().CommandText.ToString();
                string delete = DbCommandBuilder.GetDeleteCommand().CommandText.ToString();


                return adapter.Update(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void readDatathroughAdapter(string query, DataTable tblName)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                adapter = new SqlDataAdapter(command);
                adapter.Fill(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static SqlDataReader readDatathroughReader(string query)
        {
            //DataReader used to sequentially read data from a data source
            SqlDataReader reader;

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static int executeQuery(SqlCommand dbCommand)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }

                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;


                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
