using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tiger_Services_Ticketing_App
{
    public partial class Injury : Form
    {
        private SqlConnection sqlcon;
        private string com_ID;
        private string pcname = "";
        public Injury()
        {
            InitializeComponent();
            pcname = System.Environment.MachineName;
            sqlcon = new SqlConnection(@"Data Source=" + pcname + ";Initial Catalog=TS_Ticketing;Persist Security Info=True;User ID=sa;Password=TSSQL_db");

        }

        private void Injury_Load(object sender, EventArgs e)
        {

        }
    }
}
