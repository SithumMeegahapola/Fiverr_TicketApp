using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tiger_Services_Ticketing_App
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer_Complaints cc = new Customer_Complaints();
            cc.Show();
            this.Hide();
            cc.FormClosing += this.OpendFormClosing;
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            Customer_Compliment ccl = new Customer_Compliment();
            this.Hide();
            ccl.Show();
            ccl.FormClosing += OpendFormClosing;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Injury inj = new Injury();
            this.Hide();
            inj.Show();
            inj.FormClosing += OpendFormClosing;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Third_Party tpc = new Third_Party();
            this.Hide();
            tpc.Show();
            tpc.FormClosing += OpendFormClosing;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
           
           
        }

        private void OpendFormClosing(Object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        private void formloadmethod()
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
            f1.FormClosing += OpendFormClosing;
          
        }
    }
}
