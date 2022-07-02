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
    public partial class add_items : Form
    {
        public add_items()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void add_items_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string name = txt_name.Text;
            string price = txt_price.Text;
            string quantity = txt_quan.Text;
            string category = txt_cat.Text;
            if (name == "")
            {
                MessageBox.Show("Please Enter item's name");

            }
            else
            {
                UsersDA.insert_product(name, price, category, quantity);
                MessageBox.Show("Item added");
                txt_name.Text = "";
                txt_price.Text = "";
                txt_quan.Text = "";
                txt_cat.Text = "";
            }



        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
