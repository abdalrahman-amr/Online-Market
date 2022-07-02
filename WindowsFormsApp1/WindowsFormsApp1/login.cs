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
namespace WindowsFormsApp1
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            DBHelper.EstablishConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string type;
            if (rdbtn_admin.Checked)
            {
                type = "admins";
                User aUser = UsersDA.RetrieveUser(type, username);
                if (aUser is null)
                {
                    MessageBox.Show("No such account exists. Please try again");
                }
                else
                {
                    if (aUser.Password.Equals(password))
                    {
                        MessageBox.Show("Login Success\n Welcome Admin");
                        admin_main x = new admin_main();
                        x.Show();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed. Please try again");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
            }
            else if (rdbtn_client.Checked)
            {
                type = "users";
                User aUser = UsersDA.RetrieveUser(type, username);
                if (aUser is null)
                {
                    MessageBox.Show("No such account exists. Please try again");
                }
                else
                {
                    if (aUser.Password.Equals(password))
                    {
                        MessageBox.Show("Login Success\n Welcome Client");
                        client_main x = new client_main(username);
                        x.Show();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed. Please try again");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an account type");

            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            signup x = new signup();
            x.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
