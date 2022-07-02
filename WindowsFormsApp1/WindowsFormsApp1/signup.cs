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
    public partial class signup : Form
    {
        public signup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txt_name.Text;
            string pass = txt_pass.Text;
            UsersDA.new_user(name, pass);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
