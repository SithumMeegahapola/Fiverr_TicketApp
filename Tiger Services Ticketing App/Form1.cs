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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-2L7SMKJ; Initial Catalog=TS_Ticketing; Persist Security Info=True; User ID=sa; Password=TSSQL_db");
            sqlcon.Open();
            string query = "SELECT * from App_Users WHERE (User_Name='" + textBox1.Text + "') AND (User_Password='" + textBox2.Text+"')";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count>0)
            {
                MainWindow.ActiveForm.Show();
                this.Dispose();
                this.Close();
                sqlcon.Close();
            }
            else
            {
                MessageBox.Show("Invalid Credentiols");
                sqlcon.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
