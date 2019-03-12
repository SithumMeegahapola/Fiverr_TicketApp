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
    public partial class Customer_Complaints : Form
    {
        public Customer_Complaints()
        {
            InitializeComponent();
        }

        private void Customer_Complaints_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd mm yyyy";

            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.AddExtension = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "*.jpeg|*.png|*.pdf|*.docx";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string filename in openFileDialog1.FileNames)
                {

                }
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
                //uploading the data to the Server

                



            }
            else
            {
                MessageBox.Show("Fill All the Details");
            }
        }
    }
}
