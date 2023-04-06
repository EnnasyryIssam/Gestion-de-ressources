using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace final_project.DB
{
    public class DbConnection
    {
        private SqlConnection connection;
        public DbConnection()
        {
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\Desktop\final_project\final_project\App_Data\db_faculte.mdf;Integrated Security=True";
                connection.Open();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public SqlConnection Connection { get => connection; set => connection = value; }
    }
}