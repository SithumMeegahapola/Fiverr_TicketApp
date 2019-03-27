using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tiger_Services_Ticketing_App
{
    public partial class Third_Party : Form
    {

        private MySqlConnection mycon;
        private string com_ID;
        private string constring;
       
        public Third_Party()
        {
            InitializeComponent();
            constring = "SERVER= 206.189.145.254;PORT=3306;DATABASE=TS_Ticketing;UID=root;PASSWORD=pass";
            try
            {
                mycon = new MySqlConnection();
                mycon.ConnectionString = constring;
                mycon.Open();
                mycon.Close();


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Third_Party_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd mm yyyy";

            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;
            loadID();

        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            resetAll();
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



                    
                    string query = "INSERT INTO `Third_Party_Claim`(`Claim_Code`, `Name`, `Phone`, `Address`, `EMail`, `P_R_Name`, `P_R_Phone`, `P_R_EMail`, `Details`, `Time_Of_Claim`, `Date_Of_Claim`, `Uploaded_Files`, `Ticket_Status`)" +
                                   "VALUES ('" + com_ID + "','" + name + "','" + phone + "','" + address + "','" + email + "','" + PRName + "','" + PRPhone + "','" + PREmail + "','" + Details + "','" + Time + "','" + Date + "','" + uploadedfilenames + "','Open');";
                    MySqlCommand cmd = new MySqlCommand(query, mycon);
                    try
                    {
                        mycon.Open();
                        int number = cmd.ExecuteNonQuery();
                        mycon.Close();
                        if (number > 0)
                        {
                            MessageBox.Show("Ticket has been saved successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mycon.Close();
                            if (!label12.Text.Equals("No Files Selected"))
                            {
                                //copying the files to folder
                                string path = Path.Combine(Environment.CurrentDirectory, @"Uploaded_Data\Third_Party_Claim\");

                                string destpath = path + com_ID;
                                string sourcefile = "";
                                string destfile = "";
                                foreach (string filename in openFileDialog1.FileNames)
                                {
                                    sourcefile = filename;

                                    destfile = System.IO.Path.Combine(destpath, openFileDialog1.SafeFileName);

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
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"Uploaded_Data\");
                if (!System.IO.Directory.Exists(path + "Third_Party_Claim"))
                {
                    System.IO.Directory.CreateDirectory(path + "Third_Party_Claim");

                }
                string finalpath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Uploaded_Data\Third_Party_Claim\");

                System.IO.Directory.CreateDirectory(finalpath + com_ID);



            }
        }
        private void loadID()
        {

            mycon.Open();
            string getquery = "SELECT * FROM `Third_Party_Claim` ";
            MySqlDataAdapter sd = new MySqlDataAdapter(getquery, mycon);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            int count = dt.Rows.Count + 1;
            //getting the entered Complaints to create the new Complaint ID
            com_ID = "";
            if (count < 10)
            {
                com_ID = "Claim_0000" + count;
            }
            else if (count < 100)
            {
                com_ID = "Claim_000" + count;
            }
            else if (count < 1000)
            {
                com_ID = "Claim_00" + count;
            }
            else if (count < 10000)
            {
                com_ID = "Claim_0" + count;
            }
            else if (count < 100000)
            {
                com_ID = "Claim_" + count;
            }

            this.Text = "Customer Claim ID - " + com_ID;


            mycon.Close();
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                MessageBox.Show("Enter A number");
                
                textBox2.Focus();
                textBox2.Text = "";
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                MessageBox.Show("Enter A number");
               
                textBox6.Focus();
                textBox2.Text = "";
                e.Handled = true;
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            

        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
    }
}
