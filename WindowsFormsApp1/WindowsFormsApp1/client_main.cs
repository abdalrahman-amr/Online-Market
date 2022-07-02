using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApp1
{
    public partial class client_main : Form
    {
        string x;
        DataTable dt2 = new DataTable();
        private Socket client;
        private byte[] data = new byte[1024];
        private int size = 1024;
        public client_main(string username)
        {
            InitializeComponent();
            x= username;
            view_items.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ///////////////////
            ///////////////////
            //////////////////
            Socket newsock = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),
            5020);
            newsock.BeginConnect(iep, new AsyncCallback(Connected),
            newsock);
        }
        private void Connected(IAsyncResult ar)
        {
            client = (Socket)ar.AsyncState;
            try
            {
                client.EndConnect(ar);
                client.BeginReceive(data, 0, size, SocketFlags.None, new
                AsyncCallback(ReceiveData), client);
            }
            catch (SocketException)
            {
                MessageBox.Show( "Error connecting");

            }
        }
        private void ReceiveData(IAsyncResult ar)
        {
            Socket remote = (Socket)ar.AsyncState;
            int recv = 1;
            //while(recv!=0)
            recv = remote.EndReceive(ar);

            while (recv != 0)
            {
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                ///////////////
                ////////////////
                //////////////    received data msgs msgs gmsg



                recv = remote.Receive(data);
            }

        }
        private void SendData(IAsyncResult ar)
        {
            try
            {

                Socket remote = (Socket)ar.AsyncState;
                int sent = remote.EndSend(ar);
                remote.BeginReceive(data, 0, size, SocketFlags.None, new
                AsyncCallback(ReceiveData), remote);


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void clickingsending(string globally)
        {
            byte[] message = Encoding.ASCII.GetBytes(globally);
            // client.Send(message);
            /////////////////
            /////////////////////
            //////////////////
            byte[] recvdata = new byte[4 * 1024];


            client.BeginSend(message, 0, message.Length, SocketFlags.None, new
            AsyncCallback(SendData), client);
        }

        















        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void client_main_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string query = "select id,name,category,price,stockQuantity from user_transaction.product;";
            DataTable dt = new DataTable();
            dt = DBHelper.RunQuery2(query);
            view_items.DataSource = dt;
            
            /////////////////////////////////////////
            string query2 = "select Distinct product.id,product.name,quantity from has_in_cart,product,users where product.id=has_in_cart.product_id and user_username = '" + x +"';" ;
            dt2 = DBHelper.RunQuery2(query2);
            view_cart.DataSource = dt2;

        }

        private void view_items_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            string id = view_items.SelectedRows[0].Cells["id"].Value.ToString();
            string quantity = UsersDA.check_zero(id);
            if (quantity == "0")
            {
                MessageBox.Show("Stock is empty");

            }
            else {
                UsersDA.insert_to_cart(x, id);

            }
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in dt2.Rows)
            {
                string id = dr["id"].ToString();
                string quantity = dr["quantity"].ToString();
                UsersDA.update_product_final( id, quantity);
            }
            UsersDA.delete_cart(x);
            string query = "select id,name,category,price,stockQuantity from user_transaction.product;";
            DataTable dt = new DataTable();
            dt = DBHelper.RunQuery2(query);
            view_items.DataSource = dt;

            /////////////////////////////////////////
            string query2 = "select Distinct product.id,product.name,quantity from has_in_cart,product,users where product.id=has_in_cart.product_id and user_username = '" + x + "';";
            dt2 = DBHelper.RunQuery2(query2);
            view_cart.DataSource = dt2;

        }

        private void view_cart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
       







        }

        private void button4_Click(object sender, EventArgs e)
        {
            UsersDA.delete_cart(x);
            string query2 = "select Distinct product.name,quantity from has_in_cart,product,users where product.id=has_in_cart.product_id and user_username = '" + x + "';";
            dt2 = DBHelper.RunQuery2(query2);
            view_cart.DataSource = dt2;

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string id = txt_id.Text;
            string quantity = txt_quan.Text;
            string stockQuantity = UsersDA.check_zero(id);
            if(stockQuantity == "")
            {
                MessageBox.Show("Please Enter quantity");

            }
            else if (stockQuantity == "0")
            {
                MessageBox.Show("Stock is empty");

            }
            else if (Int32.Parse(stockQuantity)< Int32.Parse(quantity))
            {
                MessageBox.Show("Quantity required is not available");
            }
            else
            {
                UsersDA.insert_to_cart(x, id,quantity);
                string query2 = "select Distinct product.id,product.name,quantity from has_in_cart,product,users where product.id=has_in_cart.product_id and user_username = '" + x + "';";
                dt2 = DBHelper.RunQuery2(query2);
                view_cart.DataSource = dt2;
            }

        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txt_quan_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
                    }

        private void button5_Click(object sender, EventArgs e)
        {   string search = txt_srch.Text;

            string query = "select id,name,category,price,stockQuantity from user_transaction.product where name like '" +search +"%'"; 
            DataTable dt = new DataTable();
            dt = DBHelper.RunQuery2(query);
            view_items.DataSource = dt;
        }
    }
}
