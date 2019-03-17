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
            sqlcon.Close();
        }
    }
}
