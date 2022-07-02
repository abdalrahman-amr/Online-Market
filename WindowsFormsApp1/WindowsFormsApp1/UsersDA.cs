using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1;
namespace WindowsFormsApp1
{
    public class UsersDA
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        public static User RetrieveUser(string type,string username)
        {
            string query;
            if (type == "admins") {
                query = "SELECT * FROM user_transaction.admins where username = (@username) limit 1";

            }
            else {
                 query = "SELECT * FROM user_transaction.users where username = (@username) limit 1";
            }
            cmd = DBHelper.RunQuery(query, username);
            User aUser = null;
            if (cmd != null)
            {
                dt = new DataTable();
                sda = new MySqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string uName = dr["username"].ToString();
                    string password = dr["password"].ToString();
                    aUser = new User(uName, password);
                }
            }
            return aUser;
        }
        public static void insert_product( string name ,string price, string category, string stockQuantity)
        {
            string query = "INSERT INTO user_transaction.product(name, category, price , stockQuantity , soldQuantity) VALUES ((@input1), (@input3), (@input2), (@input4) , 0);";
            cmd = DBHelper.RunInsert(query, name, price, category, stockQuantity);
        }
        public static void update_product(string name, string price, string category, string stockQuantity)
        {   string pricetxt;
            string cattxt;
            string quantxt;
            if (price == "")
            {
                pricetxt = "";
            }
            else { pricetxt = "product.price = (@input2)"; }
            if (category == "")
            {
                cattxt = "";
            }
            else { cattxt = "product.category = (@input3)"; }
            if (stockQuantity == "")
            {
                quantxt = "";
            }
            else { quantxt = "product.stockQuantity = (@input4)"; }

            string query = "UPDATE user_transaction.product SET "+ pricetxt  + " WHERE product.name = (@input1);";
            cmd = DBHelper.RunInsert(query, name, price, category, stockQuantity);
            query = "UPDATE user_transaction.product SET "  + cattxt  + " WHERE product.name = (@input1);";
            cmd = DBHelper.RunInsert(query, name, price, category, stockQuantity);
            query = "UPDATE user_transaction.product SET " + quantxt + " WHERE product.name = (@input1);";
            cmd = DBHelper.RunInsert(query, name, price, category, stockQuantity);
        }
        public static void delete_product(string name, string price, string category, string stockQuantity)
        {
            string query = "DELETE FROM  user_transaction.product WHERE product.name = (@input1);";
            cmd = DBHelper.RunInsert(query, name, price, category, stockQuantity);
        }
        public static void insert_to_cart(string user_username, string id,string quantity)
        {
            string query = "INSERT INTO user_transaction.has_in_cart(user_username, product_id,quantity ) VALUES ((@input1),  (@input2),(@input3) );";
            cmd = DBHelper.RunInsert(query, user_username, id, quantity, "");
        }
        public static void delete_cart(string name)
        {
            string query = "DELETE FROM  user_transaction.has_in_cart WHERE user_username = (@input1);";
            cmd = DBHelper.RunInsert(query, name,"","","" );
        }
        public static void delete_into_cart(string name)
        {
            string query = "DELETE FROM  user_transaction.has_in_cart WHERE  name = (@input1);";
            cmd = DBHelper.RunInsert(query, name, "", "", "");
        }
        public static void update_product_final(string id, string quantity )
        {
           
            string query = "UPDATE user_transaction.product set product.stockQuantity = (product.stockQuantity - (@input2)) where product.id = (@input1)";
            cmd = DBHelper.RunInsert2(query, id, quantity);
            query = "UPDATE user_transaction.product set product.soldQuantity = (product.soldQuantity + (@input2)) where product.id = (@input1)";
            cmd = DBHelper.RunInsert2(query, id, quantity);
          
        }
        public static string check_zero(string id)
        {
            string query = "select stockQuantity from product where id = (@input1)";
            DataTable dt = new DataTable();
            dt = DBHelper.RunQuery3(query,id);
            string name= "";
            foreach (DataRow dr in dt.Rows)
            {
                name = dr["stockQuantity"].ToString(); 
            }

            return name;
        }
        public static void new_user(string user_username, string password)
        {
            string query = "INSERT INTO user_transaction.users(username, password ) VALUES ((@input1),  (@input2) );";
            cmd = DBHelper.RunInsert3(query, user_username, password);
        }
    }
}
