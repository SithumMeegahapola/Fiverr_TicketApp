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
using System.IO;

namespace Tiger_Services_Ticketing_App
{
    public partial class Customer_Complaints : Form
    {
        SqlConnection sqlcon;
        string com_ID;
        public Customer_Complaints()
        {
            InitializeComponent();
            sqlcon = new SqlConnection(@"Data Source=DESKTOP-2L7SMKJ;Initial Catalog=TS_Ticketing;Persist Security Info=True;User ID=sa;Password=TSSQL_db");

        }

        private void Customer_Complaints_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd mm yyyy";

            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;

            sqlcon.Open();
            string getquery = "SELECT * FROM Customer_Complaints ";
            SqlDataAdapter sd = new SqlDataAdapter(getquery, sqlcon);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            int count = dt.Rows.Count;
            //getting the entered Complaints to create the new Complaint ID
            com_ID = "";

            if (count < 10)
            {
                com_ID = "Complaint0000" + count + 1;
            }
            else if (count < 100)
            {
                com_ID = "Complaint000" + count + 1;
            }
            else if (count < 1000)
            {
                com_ID = "Complaint00" + count + 1;
            }
            else if (count < 10000)
            {
                com_ID = "Complaint0" + count + 1;
            }
            else if (count < 100000)
            {
                com_ID = "Complaint" + count + 1;
            }

            Customer_Complaints.ActiveForm.Text = "Customer Complaints ID - " + com_ID;



        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.AddExtension = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "*.jpeg|*.png|*.pdf|*.doc";


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                label12.Text = "";
                foreach (string filename in openFileDialog1.SafeFileNames)
                {
                    label12.Text += filename + " ";

                }
                string path = Path.Combine(Environment.CurrentDirectory, @"Uploaded_Data\");

                System.IO.Directory.CreateDirectory(path + com_ID);


            }
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
                //adding the filnames to save on DB
                string uploadedfilenames="";
                
                foreach (string filename in openFileDialog1.SafeFileNames)
                {

                    uploadedfilenames += filename+",";

                }

                MessageBox.Show(uploadedfilenames);
                //uploading the data to the Server


                //getting the text Boxes inputs
                string name = textBox1.Text;
                int phone = int.Parse(textBox2.Text);
                string address = textBox3.Text;
                string email = textBox4.Text;
                string PRName = textBox5.Text;
                int PRPhone = int.Parse(textBox6.Text);
                string PREmail = textBox7.Text;
                string Details = textBox8.Text;
                string Date = dateTimePicker1.Value.ToShortDateString();
                string Time = dateTimePicker2.Value.ToShortTimeString();




                //string query = "INSERT INTO  Customer_Complaints (Complaint_ID , Name, Phone, Address, EMail, P_R_Name, P_R_Phone, P_R_EMail, Details, Time_Of_Complaint, Date_Of_Complaint, Uploaded_Files) " +
                //               "VALUES (" + com_ID + ", " + name + ", " + phone + ", " + address + ", " + email + ", " + PRName + ", " + PRPhone + ", " + PREmail + ", " + Details + ", " + Time + ", " + Date + ", "+ uploadedfilenames+");";
                //SqlCommand cmd = new SqlCommand(query, sqlcon);

                SqlCommand command = new SqlCommand("insert",sqlcon);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Complaint_ID", com_ID);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Address",address);
                command.Parameters.AddWithValue("@EMail", email);
                command.Parameters.AddWithValue("@P_R_Name", PRName);
                command.Parameters.AddWithValue("@P_R_Phone", PRPhone);
                command.Parameters.AddWithValue("@P_R_EMail", PREmail);
                command.Parameters.AddWithValue("@Details", Details);
                command.Parameters.AddWithValue("@Time_Of_Complaint", Time);
                command.Parameters.AddWithValue("@Date_Of_Complaint", Date);
                command.Parameters.AddWithValue("@Uploaded_Files", uploadedfilenames);


                int number = command.ExecuteNonQuery();
                if (number > 0)
                {
                    MessageBox.Show("Successfull!!!! Form Reseting");
                    sqlcon.Close();

                    //copying the files to folder
                    string path = Path.Combine(Environment.CurrentDirectory, @"Uploaded_Data\");

                    string destpath = path + com_ID;
                    string sourcefile = "";
                    string destfile = "";
                    foreach (string filename in openFileDialog1.FileNames)
                    {
                        sourcefile = filename;
                        destfile = System.IO.Path.Combine(destpath, openFileDialog1.SafeFileName);

                        System.IO.File.Copy(sourcefile, destfile, true);

                        uploadedfilenames += openFileDialog1.SafeFileName + ",";

                    }



                    Customer_Complaints_Load(sender,e);
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

        private void button4_Click(object sender, EventArgs e)
        {
            MainWindow f1 = new MainWindow();
            this.Dispose();
            f1.Show();
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
    }
}
