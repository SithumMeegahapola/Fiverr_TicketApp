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
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Customer_Compliment ccl = new Customer_Compliment();
            this.Dispose();
            ccl.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Injury inj = new Injury();
            this.Dispose();
            inj.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Third_Party tpc = new Third_Party();
            this.Dispose();
            tpc.Show();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
