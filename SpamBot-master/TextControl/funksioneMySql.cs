using System;
using System.Data;
using System.Windows.Forms;
using MySqlConnector;

namespace LiveSwitch.TextControl
{
    class funksioneMySql
    {
        private static string myConnectionString = "";
        //private static string myConnectionString = "Server=databases.000webhost.com ; Port=3306 ; Database=id15608098_comepassdb ; UserID=id15608098_admin ; Password=MyDatabase1234!!!!";


        private static MySqlConnection sqlConnection = new MySqlConnection(myConnectionString);

        public void shto_user(string user_name, string pass, string email, bool admin)
        {
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                string querry = "Insert Into Users(Username, Kodi, E_Mail, Admin) Values(@emri, @pass, @mail, @admin)";

                MySqlCommand komand = new MySqlCommand(querry, sqlConnection);
                komand.Parameters.AddWithValue("@emri", user_name);
                komand.Parameters.AddWithValue("@pass", pass);
                komand.Parameters.AddWithValue("@mail", email);
                komand.Parameters.AddWithValue("@admin", admin);

                komand.ExecuteNonQuery();

                MessageBox.Show("Sukses !!!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool log_in(string user_name, string pass)
        {
            //int i = 0;

            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                string querry = "Select * From Users Where Username = @username and Kodi = @pass";

                MySqlCommand command = new MySqlCommand(querry, sqlConnection);
                command.Parameters.AddWithValue("@username", user_name);
                command.Parameters.AddWithValue("@pass", pass);
                /*command.ExecuteNonQuery();
                 * 
                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);

                i = dataTable.Rows.Count;*/

                MySqlDataReader dr = command.ExecuteReader();

                //if(i == 0)
                if (dr.Read())
                {
                    MessageBox.Show("Welcome!");
                    Login_Info.ID = int.Parse(dr["User_ID"].ToString());
                    Login_Info.U_Name = (dr["Username"].ToString());
                    Login_Info.email = (dr["E_Mail"].ToString());
                    Login_Info.admin = bool.Parse(dr["Admin"].ToString());

                    return true;
                }
                else
                {
                    MessageBox.Show("Failed To Login");
                    return false;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void shto_email(string email, int nr)
        {
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                string querry = "Insert Into Email_Sent(UserEmail, Nremial, DataDergimit) Values(@email, @nr, @data)";

                MySqlCommand komand = new MySqlCommand(querry, sqlConnection);
                komand.Parameters.AddWithValue("@email", email);
                komand.Parameters.AddWithValue("@nr", nr);
                komand.Parameters.AddWithValue("@data", DateTime.UtcNow.Date);

                komand.ExecuteNonQuery();

                //MessageBox.Show("EMAIL UPDATE!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void show_data(DataGridView dataGridView1)
        {
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                string querry = "Select UserEmail, sum(NrEmial) as Totali From Email_Sent Group By UserEmail";

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(querry, sqlConnection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
