using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSwitch.TextControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string username = bunifuMaterialTextbox6.Text;
            string pass = bunifuMaterialTextbox1.Text;

            funksioneMySql funksione = new funksioneMySql();
            bool login = funksione.log_in(username, pass);

            if(login == true)
            {

                Form1 f1 = new Form1();
                this.Hide();
                Form2 f2 = new Form2();
                f2.Show();
            }
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterToScreen();
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
