using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tiger_Services_Ticketing_App
{
    public partial class Form1 : Form
    {
        private static string logedName = "";
        MySqlConnection mycon;
        string constring;

        public static string getLogedName()
        {
            return logedName;

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            mycon.Open();
            string query = "SELECT * FROM `App_Users` WHERE `User_Name`= '" + textBox1.Text + "' AND `User_Password`= '" + textBox2.Text+"'";
            MySqlDataAdapter msda = new MySqlDataAdapter(query, mycon);
           
            DataTable dt = new DataTable();
            msda.Fill(dt);
            if(dt.Rows.Count>0)
            {
                MainWindow.ActiveForm.Show();
                logedName = textBox1.Text;
                this.Close();
                mycon.Close();
            }
            else
            {
                MessageBox.Show("Invalid Credentiols");
                mycon.Close();
                RestAll();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RestAll();
        }

        private void RestAll ()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
