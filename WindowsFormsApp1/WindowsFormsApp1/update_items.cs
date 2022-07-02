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
    public partial class update_items : Form
    {
        public update_items()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
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
                UsersDA.update_product(name, price, category, quantity);
                MessageBox.Show("Item updated");
                txt_name.Text = "";
                txt_price.Text = "";
                txt_quan.Text = "";
                txt_cat.Text = "";
            }
        }

        private void update_items_Load(object sender, EventArgs e)
        {

        }
    }
}
