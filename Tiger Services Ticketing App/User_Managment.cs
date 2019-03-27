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
using MySql.Data.MySqlClient;

namespace Tiger_Services_Ticketing_App
{
    public partial class User_Managment : Form
    {
        private MySqlConnection mycon;
        private string constring;

        private string userID;
        public User_Managment()
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

        private void User_Managment_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            loaddata();
            
        }

        private void loaddata ()
        {
            mycon.Open();
            string query = "SELECT * FROM `App_Users`";
            MySqlDataAdapter myda = new MySqlDataAdapter(query, mycon);
            
            DataTable dt = new DataTable();
            myda.Fill(dt);
            dataGridView1.DataSource = dt;

            int count = dataGridView1.Rows.Count+1;
             if(count<10)
            {
                userID = "USR-00" + count;
;           }
             else if (count <100)
            {
                userID = "USR-0" + count;
            }
             else if (count <1000)
            {
                userID = "USR-" + count;
            }
            textBox4.Text = userID;
            

            mycon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DialogResult rs = MessageBox.Show("Are you sure you want to delete this user?", "Sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (rs == DialogResult.OK)
                {
                    int id = dataGridView1.SelectedRows[0].Index;
                    DataGridViewRow dg = dataGridView1.Rows[id];
                    string Cellvalue = dg.Cells[0].Value.ToString();
                    mycon.Open();
                    string query = "Delete FROM `App_Users` WHERE `User_ID` ='" + Cellvalue + "';";
                    MySqlCommand cmd = new MySqlCommand(query, mycon);
                    int done = cmd.ExecuteNonQuery();
                    if (done > 0)
                    {
                        MessageBox.Show("User Deleted","Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        
                    }
                    else
                    {
                        MessageBox.Show("ERROR");
                    }
                    mycon.Close();
                    loaddata();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox2.Text != "" && textBox3.Text != "" )
            {
                if(textBox2.Text.Equals(textBox3.Text))
                {
                    mycon.Open();
                    string name = textBox1.Text;
                    string password = textBox3.Text;

                    string qury = "INSERT INTO `App_Users` (`User_Code`,`User_Name`,`User_Password`) " +
                                  "VALUES ('"+userID+"','" + name + "', '" + password + "');";
                    MySqlCommand cmd = new MySqlCommand(qury, mycon);
                    int done = cmd.ExecuteNonQuery();

                    if(done>0)
                    {
                        MessageBox.Show("User Added", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        resetAll("All");

                    }
                    else
                    {
                        MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        resetAll("All");
                    }
                    mycon.Close();
                    loaddata();
                }
                else
                {
                    MessageBox.Show("Password Didnt Matched", "Error",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    resetAll("Password");
                }
            }
            else
            {
                MessageBox.Show("Enter All Details To add User", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       private void resetAll(string what)
        {
            if(what.Equals("Password"))
            {
                textBox2.Text = "";
                textBox3.Text = "";
            }
            else if(what.Equals("All"))
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
            
          
        }
    }
}
