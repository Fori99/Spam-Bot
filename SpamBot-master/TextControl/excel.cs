using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSwitch.TextControl
{
    public class excel
    {
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

        public string gjej_emailin_e_rradhes(int i )
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
