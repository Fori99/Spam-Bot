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
using System.IO;

namespace LiveSwitch.TextControl
{
    class mesazhe
    {
        public void send_msg(string email, string pass, string smtp, string port, string titulli, System.Windows.Forms.ProgressBar progressBar, string mesazhi)
        {
            try
            {
                NetworkCredential login = new NetworkCredential(email, pass);

                SmtpClient client = new SmtpClient(smtp);
                client.Port = Convert.ToInt32(port);
                client.UseDefaultCredentials = false;
                client.Credentials = login;

                //HtmlDocument dokumenti_html = new HtmlDocument();
                //dokumenti_html.LoadHtml(path);
                //dokumenti_html.rea

                //string path = @"G:\Projekte\SpamBot-master\TextControl\warm_up_fushat.html";

                MailMessage message = new MailMessage();

                message.From = new MailAddress(email);
                message.Subject = titulli;
                message.IsBodyHtml = true;
                //message.Body = create_html_body(path);

                message.Body = mesazhi;
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
                    progressBar.Value = progressBar.Value + 1;

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

        public string create_html_body(string path)
        {
            string body = string.Empty;
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //StreamReader sr = new StreamReader(assembly.GetManifestResourceStream(TextControl.));
            //body = sr.ReadToEnd();

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    body = sr.ReadToEnd();
                }
                fs.Close();
            }

            return body;
        }

        public int send_template_msg(int sending_nr, int max_progress, System.Windows.Forms.Label progess_label, System.Windows.Forms.ProgressBar progressBar, string email, string pass, string smtp, string port, string titulli, string path, int email_index)
        {
            try
            {
                NetworkCredential login = new NetworkCredential(email, pass);

                SmtpClient client = new SmtpClient(smtp);
                client.Port = Convert.ToInt32(port);
                client.UseDefaultCredentials = false;
                client.Credentials = login;

                MailMessage message = new MailMessage();

                message.From = new MailAddress(email);
                message.Subject = titulli;

                message.IsBodyHtml = true;
                message.Body = create_html_body(@path);

                //excel funksione_excel = new excel();
                funksioneMySql funksione = new funksioneMySql();

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(F_Name);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                int email_list_nr = email_index;

                for (int x = 0; x < sending_nr; x++)
                {
                    if (xlRange.Cells[email_list_nr, 1] != null && xlRange.Cells[email_list_nr, 1].Value2 != null)
                    {
                        message.To.Add(xlRange.Cells[email_list_nr, 1].Value2.ToString());

                        client.Send(message);
                        message.To.Clear();

                        progressBar.Value = progressBar.Value + 1;
                        progess_label.Text = email_index.ToString() + " / " + max_progress.ToString();                        

                        email_index = email_index + 1;
                        email_list_nr = email_list_nr + 1;
                    }
                    else
                    {
                        MessageBox.Show("No More E-Mails In The List!");
                        return -1; ;
                    }                    
                }
                xlApp.Workbooks.Close();
                return email_index;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return -1;
            }
        }

        static string file_name;

        public static string F_Name
        {
            get
            {
                return file_name;
            }
            set
            {
                file_name = value;
            }
        }

        public int gjej_nr_e_emaileve_ne_liste()
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Title = "Excel File Dialog";
                fileDialog.RestoreDirectory = true;
                fileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    F_Name = fileDialog.FileName;
                }

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(F_Name);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;

                xlApp.Workbooks.Close();

                MessageBox.Show("Emails Have Been Uploaded Successfully! " + rowCount);

                return rowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }

        public string gjej_emailin_e_rradhes(int i)
        {
            i++;
            try
            {
                string email;

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(F_Name);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                {
                    email = xlRange.Cells[i, 1].Value2.ToString();
                    xlApp.Workbooks.Close();
                    return email;
                }
                else
                {
                    xlApp.Workbooks.Close();
                    return "fund@fund.fund";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "fund@fund.fund";
            }
        }

        public void afisho_serverat(DataGridView dataGridView1, DataGridView dataGridView2)
        {
            string sFile = "https://www.thetraderweb.live/MyServers/ServerList.xlsx";

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(sFile);
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            int iRow, iCol = 0; ;

            // FIRST, CREATE THE DataGridView COLUMN HEADERS.
            for (iCol = 1; iCol <= xlWorkSheet.Columns.Count; iCol++)
            {
                if (xlWorkSheet.Cells[1, iCol].value == null)
                {
                    break;      // BREAK LOOP.
                }
                else
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.HeaderText = xlWorkSheet.Cells[1, iCol].value;
                    int colIndex = dataGridView1.Columns.Add(col);        // ADD A NEW COLUMN.
                }
            }

            for (iCol = 1; iCol <= xlWorkSheet.Columns.Count; iCol++)
            {
                if (xlWorkSheet.Cells[1, iCol].value == null)
                {
                    break;      // BREAK LOOP.
                }
                else
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.HeaderText = xlWorkSheet.Cells[1, iCol].value;
                    int colIndex = dataGridView2.Columns.Add(col);        // ADD A NEW COLUMN.
                }
            }

            // ADD ROWS TO THE GRID USING EXCEL DATA.
            for (iRow = 2; iCol <= xlWorkSheet.Rows.Count; iRow++)
            {
                if (xlWorkSheet.Cells[iRow, 1].value == null)
                {
                    break;      // BREAK LOOP.
                }
                else
                {
                    // CREATE A STRING ARRAY USING THE VALUES IN EACH ROW OF THE SHEET.
                    string[] row = new string[]
                    {
                        xlWorkSheet.Cells[iRow, 1].value,
                        xlWorkSheet.Cells[iRow, 2].value.ToString(),
                        xlWorkSheet.Cells[iRow, 3].value,
                        xlWorkSheet.Cells[iRow, 4].value.ToString()
                    };

                    // ADD A NEW ROW TO THE GRID USING THE ARRAY DATA.
                    dataGridView1.Rows.Add(row);
                }
            }

            xlWorkBook.Close();
            xlApp.Quit();
        }
    }
}

