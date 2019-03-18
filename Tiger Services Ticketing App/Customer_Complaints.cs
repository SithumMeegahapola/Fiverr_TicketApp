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
        private SqlConnection sqlcon;
        private string com_ID;
        private string pcname = "";
        Boolean keywarn = false;
        public Customer_Complaints()
        {
            InitializeComponent();

            pcname = System.Environment.MachineName;
            sqlcon = new SqlConnection(@"Data Source=" + pcname + ";Initial Catalog=TS_Ticketing;Persist Security Info=True;User ID=sa;Password=TSSQL_db");

        }

        private void Customer_Complaints_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd mm yyyy";

            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;
            loadID();

            


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.AddExtension = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +
                                    "Documents (*.docx;*.pdf*)|*.docx;*.pdf";


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                label12.Text = "";
                foreach (string filename in openFileDialog1.SafeFileNames)
                {
                    label12.Text += filename + ", ";

                }
                string path = Path.Combine(Environment.CurrentDirectory, @"Uploaded_Data\");
                if (!System.IO.Directory.Exists(path + "Customer_Complaints"))
                {
                    System.IO.Directory.CreateDirectory(path + "Customer_Complaints");

                }
                string finalpath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Uploaded_Data\Customer_Compliment\");
                System.IO.Directory.CreateDirectory(finalpath + com_ID);

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
                DialogResult rs = MessageBox.Show("Are you Sure you want to submit?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (rs == DialogResult.OK)
                {
                    //adding the filnames to save on DB
                    string uploadedfilenames = "";

                    foreach (string filename in openFileDialog1.SafeFileNames)
                    {

                        uploadedfilenames += filename + ", ";

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
                    string Date = dateTimePicker1.Value.ToShortDateString();
                    string Time = dateTimePicker2.Value.ToShortTimeString();





                    string query = "INSERT INTO  Customer_Complaints(Complaint_ID , Name, Phone, Address, EMail, P_R_Name, P_R_Phone, P_R_EMail, Details, Time_Of_Complaint, Date_Of_Complaint, Uploaded_Files, Ticket_Status) " +
                                   "VALUES ('" + com_ID + "','" + name + "','" + phone + "','" + address + "','" + email + "','" + PRName + "','" + PRPhone + "','" + PREmail + "','" + Details + "','" + Time + "','" + Date + "','" + uploadedfilenames + "','Open');";



                    SqlCommand cmd = new SqlCommand(query, sqlcon);
                    try
                    {
                        sqlcon.Open();
                        int number = cmd.ExecuteNonQuery();
                        sqlcon.Close();
                        if (number > 0)
                        {
                            MessageBox.Show("Ticket has been saved successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            sqlcon.Close();
                            if (!label12.Text.Equals("No Files Selected"))
                            {
                                //copying the files to folder
                                string path = Path.Combine(Environment.CurrentDirectory, @"Uploaded_Data\Customer_Complaints\");

                                string destpath = path + com_ID;
                                string sourcefile = "";
                                string destfile = "";
                                foreach (string filename in openFileDialog1.FileNames)
                                {
                                    sourcefile = filename;
                                    MessageBox.Show(sourcefile);
                                    destfile = System.IO.Path.Combine(destpath, openFileDialog1.SafeFileName);
                                    MessageBox.Show(destfile);
                                    System.IO.File.Copy(sourcefile, destfile, true);

                                }
                            }
                            resetAll();
                            loadID();

                        }
                        else
                        {
                            MessageBox.Show("Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Contact Admin with Error - Server Insert Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
            }
            else
            {
                MessageBox.Show("Fill All the Details");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            resetAll();
            loadID();
        }



        private void loadID()
        {

            sqlcon.Open();
            string getquery = "SELECT * FROM Customer_Complaints ";
            SqlDataAdapter sd = new SqlDataAdapter(getquery, sqlcon);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            int count = dt.Rows.Count + 1;
            //getting the entered Complaints to create the new Complaint ID
            com_ID = "";

            if (count < 10)
            {
                com_ID = "Complaint_0000" + count;
            }
            else if (count < 100)
            {
                com_ID = "Complaint_000" + count;
            }
            else if (count < 1000)
            {
                com_ID = "Complaint_00" + count;
            }
            else if (count < 10000)
            {
                com_ID = "Complaint_0" + count;
            }
            else if (count < 100000)
            {
                com_ID = "Complaint_" + count;
            }

            this.Text = "Customer Complaints ID - " + com_ID;


            sqlcon.Close();
        }

        private void resetAll()
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
            label12.Text = "No Files Selected";
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
         
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                MessageBox.Show("Enter A number");

                textBox2.Focus();
                
                textBox2.Text = "";
                e.Handled=true;
              
            }


           

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                MessageBox.Show("Enter A number");
                
                textBox6.Focus();
                textBox6.Text = "";
                e.Handled = true;
            }
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
    }
}
