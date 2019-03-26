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
    public partial class TicketView : Form
    {
        private MySqlConnection mycon;
        private string constring;
        public TicketView()
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

        private void TicketView_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mycon.Open();
            string query = "Select * from `Customer_Complaints`";
          
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, mycon);
           
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.BackColor = Color.Purple;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.ForeColor = Color.White;

            mycon.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            mycon.Open();
            
            string query = "Select * from `Customer_Compliment`";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, mycon);
            
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.BackColor = Color.Purple;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.ForeColor = Color.White;
            mycon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mycon.Open();
            string query = "Select * from `Third_Party_Claim`";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, mycon);
           
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.BackColor = Color.Purple;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.ForeColor = Color.White;
            mycon.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mycon.Open();
            string query = "Select * from `Injury`";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, mycon);
         
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.BackColor = Color.Purple;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.ForeColor = Color.White;
            mycon.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count==1)
            {
               DialogResult rs = MessageBox.Show("Arre you sure you want to close this ticket?", "Sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(rs == DialogResult.OK)
                {
                    int id = dataGridView1.SelectedRows[0].Index;
                    DataGridViewRow dg = dataGridView1.Rows[id];
                    string CellvalueID = dg.Cells[0].Value.ToString();
                    string CellvalueCode = dg.Cells[1].Value.ToString();
                    
                    if(CellvalueCode.Contains("Complaint"))
                    {
                        mycon.Open();
                        string query = "UPDATE `Customer_Complaints` " +
                                        "SET `Ticket_Status` = 'Closed' " +
                                        "WHERE `Complaint_ID`= '"+ CellvalueID + "';";
                        MySqlCommand cmd = new MySqlCommand(query, mycon);
                        int done = cmd.ExecuteNonQuery();
                        if(done >0)
                        {
                            MessageBox.Show("Ticket Closed");
                           
                        }
                        else
                        {
                            MessageBox.Show("Error Try Again");
                        }
                        mycon.Close();
                        button1.PerformClick();


                    }
                    else if (CellvalueCode.Contains("Compliment"))
                    {
                        mycon.Open();
                        string query = "UPDATE  `Customer_Compliment` " +
                                        "SET `Ticket_Status` = 'Closed' " +
                                        "WHERE `Compliment_ID`= '" + CellvalueID + "';";
                        MySqlCommand cmd = new MySqlCommand(query, mycon);
                        int done = cmd.ExecuteNonQuery();
                        if (done > 0)
                        {
                            MessageBox.Show("Ticket Closed");
                            
                        }
                        else
                        {
                            MessageBox.Show("Error Try Again");
                        }
                        mycon.Close();
                        button2.PerformClick();




                    }
                    else if (CellvalueCode.Contains("Injury"))
                    {
                        mycon.Open();
                     
                        string query = "UPDATE  `Injury` " +
                                        "SET `Ticket_Status` = 'Closed' " +
                                        "WHERE `Injury_ID`= '" + CellvalueID + "';";
                        MySqlCommand cmd = new MySqlCommand(query, mycon);
                        int done = cmd.ExecuteNonQuery();
                        if (done > 0)
                        {
                            MessageBox.Show("Ticket Closed");
                            
                        }
                        else
                        {
                            MessageBox.Show("Error Try Again");
                        }
                        mycon.Close();
                        button4.PerformClick();




                    }
                    else if (CellvalueCode.Contains("Claim"))
                    {

                        mycon.Open();
                        string query = "UPDATE  `Third_Party_Claim` " +
                                        "SET `Ticket_Status` = 'Closed' " +
                                        "WHERE `Claim_ID` = '" + CellvalueID + "';";
                        MySqlCommand cmd = new MySqlCommand(query, mycon);
                        int done = cmd.ExecuteNonQuery();
                        if (done > 0)
                        {
                            MessageBox.Show("Ticket Closed");
                            
                        }
                        else
                        {
                            MessageBox.Show("Error Try Again");
                        }
                        mycon.Close();
                        button3.PerformClick();



                    }

                }
            }
            else
            {
                MessageBox.Show("Please Select a ID to Close");
            }
        }
    }
}
