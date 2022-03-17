using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace LiveSwitch.TextControl
{
    public partial class templateUserControl1 : UserControl
    {
        public string file_path;

        public templateUserControl1()
        {
            InitializeComponent();
            mesazhe mesazhe = new mesazhe();
            mesazhe.afisho_serverat(dataGridView1, dataGridView2);

            //webBrowser2.Navigate(@"G:\Projekte\SpamBot-master\TextControl\warm_up_fushat.html");
        }

        int index = 0;

        int time_S;
        //int time_m;
        bool is_time_active;

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string server_name = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string server_port = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string email = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            string pass = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

            dataGridView2.Rows.Add();
            dataGridView2.Rows[index].Cells[0].Value = server_name;
            dataGridView2.Rows[index].Cells[1].Value = server_port;
            dataGridView2.Rows[index].Cells[2].Value = email;
            dataGridView2.Rows[index].Cells[3].Value = pass;

            index = index + 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";

                //openFileDialog.FilterIndex = 2;
                //openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    file_path = openFileDialog.FileName;
                    webBrowser2.Navigate(file_path);
                }
            }
        }


        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = 0;

                int server_nr = dataGridView2.Rows.Count - 1;
                int server_index = 0;
                int email_index = 1;
                int sending_nr = 5 * server_nr;

                string server_name;
                string server_port;
                string email;
                string pass;
                string msg_subject = textBox5.Text;
                mesazhe mesazhe = new mesazhe();

                do
                {
                    reset_time();
                    is_time_active = true;
                    label6.Text = "Sending Emails";

                    if (server_index == server_nr)
                    {
                        server_index = 0;
                    }

                    textBox1.Text = server_name = dataGridView2.Rows[server_index].Cells[0].Value.ToString();
                    textBox2.Text = server_port = dataGridView2.Rows[server_index].Cells[1].Value.ToString();
                    textBox3.Text = email = dataGridView2.Rows[server_index].Cells[2].Value.ToString();
                    textBox4.Text = pass = dataGridView2.Rows[server_index].Cells[3].Value.ToString();

                    email_index = mesazhe.send_template_msg(sending_nr, progressBar1.Maximum, label7, progressBar1, email, pass, server_name, server_port, msg_subject, file_path, email_index);

                    if (email_index != -1)
                    {
                        is_time_active = false;
                        if (time_S < 60)
                        {
                            label6.Text = "System On Cooldown";
                            int sleep = (60 - time_S) * 1000;
                            //MessageBox.Show("CHILLIN");
                            Thread.Sleep(sleep);
                        }
                    }
                    server_index = server_index + 1;
                }
                while (email_index != -1);

                is_time_active = false;
                label6.Text = "Sending Done";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mesazhe msg = new mesazhe();

            progressBar1.Value = 0;
            progressBar1.Maximum = msg.gjej_nr_e_emaileve_ne_liste();
            progressBar1.Step = 1;
            label7.Text = "0 / " + progressBar1.Maximum.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (is_time_active == true)
            {
                time_S++;                
            }
            //show_time();
        }

        private void show_time()
        {
            label6.Text = String.Format("{0:00}", time_S);
            //label7.Text = String.Format("{0:00}", time_m);
        }

        private void reset_time()
        {
            time_S = 0;
            //time_m = 0;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void templateUserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
