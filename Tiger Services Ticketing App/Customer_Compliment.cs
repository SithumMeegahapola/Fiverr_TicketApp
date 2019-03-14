using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Tiger_Services_Ticketing_App
{
    public partial class Customer_Compliment : Form
    {
        SqlConnection sqlcon;
        public Customer_Compliment()
        {
            InitializeComponent();
            sqlcon = new SqlConnection(@"Data Source=DESKTOP-2L7SMKJ;Initial Catalog=TS_Ticketing;Persist Security Info=True;User ID=sa;Password=TSSQL_db");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainWindow ss = new MainWindow();
            ss.Show();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

            openFileDialog1.Reset();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //checkign all input boxes are filled and process
            if (textBox1.Text != "" &&
                textBox2.Text != "" &&
                textBox3.Text != "" &&
                textBox4.Text != "" &&
                textBox5.Text != "" &&
                textBox6.Text != "" &&
                textBox7.Text != "" &&
                textBox8.Text != ""
                )
            {
                //getting uploaded data if not empyt
                





                //uploading the data to the Server
                sqlcon.Open();
                string getquery = "SELECT * FROM Customer_Compliment ";
                SqlDataAdapter sd = new SqlDataAdapter(getquery, sqlcon);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                int count = dt.Rows.Count;
                //getting the entered Complaints to create the new Complaint ID
                string com_ID = "";

                if (count < 10)
                {
                    com_ID = "Compliment0000" + count + 1;
                }
                else if (count < 100)
                {
                    com_ID = "Complimentt000" + count + 1;
                }
                else if (count < 1000)
                {
                    com_ID = "Compliment00" + count + 1;
                }
                else if (count < 10000)
                {
                    com_ID = "Compliment0" + count + 1;
                }
                else if (count < 100000)
                {
                    com_ID = "Compliment" + count + 1;
                }

                //getting the text Boxes inputs
                string name = textBox1.Text;
                int phone = int.Parse(textBox2.Text);
                string address = textBox3.Text;
                string email = textBox4.Text;
                string PRName = textBox5.Text;
                int PRPhone = int.Parse(textBox6.Text);
                string PREmail = textBox7.Text;
                string Details = textBox8.Text;
                DateTime Date = dateTimePicker1.Value.Date;
                DateTime Time = dateTimePicker2.Value;


                string query = "INSERT INTO  Customer_Complaints (Complaint_ID , Name, Phone, Address, EMail, P_R_Name, P_R_Phone, P_R_EMail, Details, Time_Of_Complaint, Date_Of_Complaint, Uploaded_Files) " +
                               "VALUES (" + com_ID + ", " + name + ", " + phone + ", " + address + ", " + email + ", " + PRName + ", " + PRPhone + ", " + PREmail + ", " + Details + ", " + Date + ", " + Time + ")";
                SqlCommand cmd = new SqlCommand(query, sqlcon);

                int number = cmd.ExecuteNonQuery();
                if (number > 0)
                {
                    MessageBox.Show("Successfull!!!! Form Reseting");
                    sqlcon.Close();
                    button3.PerformClick();
                }
                else
                {
                    MessageBox.Show("Error Contact Admins");
                }





            }
            else
            {
                MessageBox.Show("Fill All the Details");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
        }
    }
}
