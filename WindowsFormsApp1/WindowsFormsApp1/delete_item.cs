using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class delete_item : Form
    {
        public delete_item()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txt_name.Text;
            if (name == "")
            {
                MessageBox.Show("Please Enter item's name");

            }
            else
            {

                UsersDA.delete_product(name, "", "", "");
                MessageBox.Show("Item deleted");
                txt_name.Text = "";
               
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
