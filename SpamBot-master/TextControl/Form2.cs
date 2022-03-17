using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace LiveSwitch.TextControl
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Height = button4.Height;
            panel4.Top = button4.Top;
            userControl21.Visible = false;
            //userControl41.Visible = false;
            templateUserControl11.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Login_Info.admin == true)
            {
                panel4.Height = button8.Height;
                panel4.Top = button8.Top;
                userControl21.Visible = true;
                userControl21.BringToFront();
                //userControl41.Visible = false;
                templateUserControl11.Visible = false;
            }
            else
            {
                MessageBox.Show("Sorry, you do not have admin permissions !");
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dashboard Is Not Available For The Moment!");
            /*if (Login_Info.admin == true)
            {
                panel4.Height = button7.Height;
                panel4.Top = button7.Top;
                //userControl41.Visible = true;
                //userControl41.BringToFront();
                //userControl21.Visible = false;
                //templateUserControl11.Visible = false;
            }
            else
            {
                MessageBox.Show("Sorry, you do not have admin permissions !");
            }*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel4.Height = button1.Height;
            panel4.Top = button1.Top;
            templateUserControl11.Visible = true;
            templateUserControl11.BringToFront();
            userControl21.Visible = false;
            //userControl41.Visible = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            templateUserControl11.Hide();
            userControl21.Hide();
            //userControl41.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string smtp = bunifuMaterialTextbox1.Text;
                string port = bunifuMaterialTextbox2.Text;
                string email = bunifuMaterialTextbox3.Text;
                string pass = bunifuMaterialTextbox4.Text;
                string titulli = bunifuMaterialTextbox5.Text;
                string mesazhi = editor.BodyText;

                NetworkCredential login = new NetworkCredential(email, pass);

                SmtpClient client = new SmtpClient(smtp);
                client.Port = Convert.ToInt32(port);
                client.UseDefaultCredentials = false;
                client.Credentials = login;

                MailMessage message = new MailMessage();

                message.From = new MailAddress(email);
                message.Subject = titulli;
                message.IsBodyHtml = true;

                message.Body = editor.BodyText;
                message.To.Add(email);

                excel funksione_excel = new excel();
                funksioneMySql funksione = new funksioneMySql();

                int i = 1;
                message.To.Add(funksione_excel.gjej_emailin_e_rradhes(i));

                while (message.To.ToString() != "fund@fund.fund")
                {
                    i = i + 1;

                    client.Send(message);
                    //funksione.shto_email(email, 1);
                    progressBar1.Value = progressBar1.Value + 1;

                    message.To.Clear();
                    message.To.Add(funksione_excel.gjej_emailin_e_rradhes(i));
                }
                MessageBox.Show("Messages Have Been Sent Successfully!");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void label9_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            excel ex = new excel();

            progressBar1.Value = 0;
            progressBar1.Maximum = ex.gjej_nr_e_emaileve_ne_liste();
            progressBar1.Step = 1;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void templateUserControl11_Load(object sender, EventArgs e)
        {

        }
    }
}