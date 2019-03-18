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
    public partial class User_Managment : Form
    {
        private SqlConnection sqlcon;
        string userID = "";
        private string pcname = "";
        public User_Managment()
        {
            InitializeComponent();
            pcname = System.Environment.MachineName;
            sqlcon = new SqlConnection(@"Data Source=" + pcname + ";Initial Catalog=TS_Ticketing;Persist Security Info=True;User ID=sa;Password=TSSQL_db");

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
            sqlcon.Open();
            string query = "select * from App_Users";
            SqlDataAdapter sdp = new SqlDataAdapter(query, sqlcon);
            SqlCommandBuilder cmdb = new SqlCommandBuilder(sdp);
            DataTable dt = new DataTable();
            sdp.Fill(dt);
            dataGridView1.DataSource = dt;

            int count = dataGridView1.Rows.Count+1;
            if(count<10)
            {
                userID = "U-00" + count;
            }
            else if (count<100)
            {
                userID = "U-0" + count;
            }
            else if (count<1000)
            {
                userID = "U-" + count;
            }
            textBox4.Text = userID;
            sqlcon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DialogResult rs = MessageBox.Show("Arre you sure you want to close this ticket?", "Sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (rs == DialogResult.OK)
                {
                    int id = dataGridView1.SelectedRows[0].Index;
                    DataGridViewRow dg = dataGridView1.Rows[id];
                    string Cellvalue = dg.Cells[0].Value.ToString();
                    sqlcon.Open();
                    string query = "Delete FROM App_Users WHERE User_ID ='" + Cellvalue + "';";
                    SqlCommand cmd = new SqlCommand(query, sqlcon);
                    int done = cmd.ExecuteNonQuery();
                    if (done > 0)
                    {
                        MessageBox.Show("User Deleted","Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        
                    }
                    else
                    {
                        MessageBox.Show("ERROR");
                    }
                    sqlcon.Close();
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
                    sqlcon.Open();
                    string name = textBox1.Text;
                    string password = textBox3.Text;

                    string qury = "INSERT INTO App_Users(User_ID,User_Name,User_Password) " +
                                  "VALUES ('" + userID + "', '" + name + "', '" + password + "');";
                    SqlCommand cmd = new SqlCommand(qury, sqlcon);
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
                    sqlcon.Close();
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
