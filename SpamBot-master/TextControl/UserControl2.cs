using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using System.Data.SqlClient;

namespace LiveSwitch.TextControl
{
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            string username = bunifuMaterialTextbox1.Text;
            string pass1 = bunifuMaterialTextbox2.Text;
            string pass2 = bunifuMaterialTextbox3.Text;
            string email = bunifuMaterialTextbox4.Text;

            bool admin;

            if(bunifuCheckbox1.Checked == true)
            {
                admin = true;
            }
            else 
            {
                admin = false;
            }

            if(pass1 == pass2)
            {
                funksioneMySql funksioneMySql = new funksioneMySql();
                funksioneMySql.shto_user(username, pass1, email, admin);
            }
            else
            {
                MessageBox.Show("Fjalekalimi nuk perputhet!");
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuMaterialTextbox3_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}
