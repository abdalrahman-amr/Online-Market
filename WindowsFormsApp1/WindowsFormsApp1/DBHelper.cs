using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;




namespace WindowsFormsApp1
{
    public static class DBHelper
    {
        private static MySqlConnection connection;
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        /*
         * Job is to establish connection to the database
         */
        public static void EstablishConnection()
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "127.0.0.1";
                builder.UserID = "root";
                builder.Password = "Aa@37122945";
                builder.Database = "user_transaction";
                builder.SslMode = MySqlSslMode.None;
                connection = new MySqlConnection(builder.ToString());
                MessageBox.Show("Database connection successfull", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("connection Failed");
            }
        }

        public static MySqlCommand RunQuery(string query, string username)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
        public static MySqlCommand RunInsert(string query, string input1 ,string input2, string input3,string input4)
        { 
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@input1", input1);
                    cmd.Parameters.AddWithValue("@input2", input2);
                    cmd.Parameters.AddWithValue("@input3", input3);
                    cmd.Parameters.AddWithValue("@input4", input4);

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }
        public static MySqlCommand RunInsert2(string query, string input1, string input2)
        {
            int q = Int32.Parse(input2);
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@input1", input1);
                    cmd.Parameters.AddWithValue("@input2", q);


                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        public static DataTable RunQuery2(string query)
        {
            DataTable table = new DataTable();

            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;

                    var reader = cmd.ExecuteReader();
                    table.Load(reader);

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return table;
        }
        public static DataTable RunQuery3(string query,string id )
        {
            DataTable table = new DataTable();

            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@input1", id);


                    var reader = cmd.ExecuteReader();
                    table.Load(reader);

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return table;
        }
        public static MySqlCommand RunInsert3(string query, string input1, string input2)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@input1", input1);
                    cmd.Parameters.AddWithValue("@input2", input2);
                   

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New account created successfully");                   
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Username already exists");
                connection.Close();
            }
            return cmd;
        }

    }
}
