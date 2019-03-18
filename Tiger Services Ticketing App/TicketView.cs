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
    public partial class TicketView : Form
    {
        SqlConnection sqlcon;
        public TicketView()
        {
            InitializeComponent();
            
            String pcname = System.Environment.MachineName;
            sqlcon = new SqlConnection(@"Data Source=" + pcname + ";Initial Catalog=TS_Ticketing;Persist Security Info=True;User ID=sa;Password=TSSQL_db");
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
            sqlcon.Open();
            string query = "Select * from Customer_Complaints";
          
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlcon);
            SqlCommandBuilder cmdbuilder = new SqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.BackColor = Color.Purple;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.ForeColor = Color.White;

            sqlcon.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            
            string query = "Select * from Customer_Compliment";

            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlcon);
            SqlCommandBuilder cmdbuilder = new SqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.BackColor = Color.Purple;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.ForeColor = Color.White;
            sqlcon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            string query = "Select * from Third_Party_Claim";

            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlcon);
            SqlCommandBuilder cmdbuilder = new SqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.BackColor = Color.Purple;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.ForeColor = Color.White;
            sqlcon.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sqlcon.Open();
            string query = "Select * from Injury";

            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlcon);
            SqlCommandBuilder cmdbuilder = new SqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.BackColor = Color.Purple;
            dataGridView1.Columns["Ticket_Status"].DefaultCellStyle.ForeColor = Color.White;
            sqlcon.Close();
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
                    string Cellvalue = dg.Cells[0].Value.ToString();
                    
                    if(Cellvalue.Contains("Complaint"))
                    {
                        sqlcon.Open();
                        string query = "UPDATE Customer_Complaints " +
                                        "SET Ticket_Status = 'Closed' " +
                                        "WHERE Complaint_ID= '"+Cellvalue+"';";
                        SqlCommand cmd = new SqlCommand(query, sqlcon);
                        int done = cmd.ExecuteNonQuery();
                        if(done >0)
                        {
                            MessageBox.Show("Ticket Closed");
                           
                        }
                        else
                        {
                            MessageBox.Show("Error Try Again");
                        }
                        sqlcon.Close();
                        button1.PerformClick();


                    }
                    else if (Cellvalue.Contains("Compliment"))
                    {
                        sqlcon.Open();
                        string query = "UPDATE  Customer_Compliment " +
                                        "SET Ticket_Status = 'Closed' " +
                                        "WHERE Compliment_ID= '" + Cellvalue + "';";
                        SqlCommand cmd = new SqlCommand(query, sqlcon);
                        int done = cmd.ExecuteNonQuery();
                        if (done > 0)
                        {
                            MessageBox.Show("Ticket Closed");
                            
                        }
                        else
                        {
                            MessageBox.Show("Error Try Again");
                        }
                        sqlcon.Close();
                        button2.PerformClick();




                    }
                    else if (Cellvalue.Contains("Injury"))
                    {
                        sqlcon.Open();
                        MessageBox.Show(Cellvalue);
                        string query = "UPDATE  Injury " +
                                        "SET Ticket_Status = 'Closed' " +
                                        "WHERE Injury_ID= '" + Cellvalue + "';";
                        SqlCommand cmd = new SqlCommand(query, sqlcon);
                        int done = cmd.ExecuteNonQuery();
                        if (done > 0)
                        {
                            MessageBox.Show("Ticket Closed");
                            
                        }
                        else
                        {
                            MessageBox.Show("Error Try Again");
                        }
                        sqlcon.Close();
                        button4.PerformClick();




                    }
                    else if (Cellvalue.Contains("Claim"))
                    {

                        sqlcon.Open();
                        string query = "UPDATE  Third_Party_Claim " +
                                        "SET Ticket_Status = 'Closed' " +
                                        "WHERE Claim_ID = '" + Cellvalue + "';";
                        SqlCommand cmd = new SqlCommand(query, sqlcon);
                        int done = cmd.ExecuteNonQuery();
                        if (done > 0)
                        {
                            MessageBox.Show("Ticket Closed");
                            
                        }
                        else
                        {
                            MessageBox.Show("Error Try Again");
                        }
                        sqlcon.Close();
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
