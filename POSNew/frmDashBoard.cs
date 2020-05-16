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
using System.Windows.Forms.DataVisualization.Charting;

namespace POSNew
{
    public partial class frmDashBoard : Form
    {

        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        public frmDashBoard()
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            LoadChart();
        }

        private void frmDashBoard_Resize(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
        }

        private void frmDashBoard_Load(object sender, EventArgs e)
        {

        }
        public void LoadChart()
        {
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("select Year (sdate) as year, SUM(IFNULL(total,0.0)) as total from tblcart where status like 'Sold' group by Year(sdate)", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Sales");
            chart1.DataSource = ds.Tables["Sales"];
            Series series1 = chart1.Series["Series1"];
            series1.ChartType = SeriesChartType.Doughnut;
            series1.Name = "SALES";

            var chart = chart1;
            chart.Series[series1.Name].XValueMember = "year";
            chart.Series[series1.Name].YValueMembers = "total";
            chart.Series[0].IsValueShownAsLabel = true;
           // chart.Series[0].LegendText = "#"
            
            con.Close();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }


    }
}
