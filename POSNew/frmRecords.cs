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
    public partial class frmRecords : Form
    {

        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        public frmRecords()
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadRecord()
        { 
            int i=0;
            con.Open();
            dataGridView1.Rows.Clear();
            if (cboTopSelect.Text == "SORT BY QTY")
            {
                cmd = new MySqlCommand("select pcode, pdesc,ifnull(sum(qty),0) as qty, ifnull(sum(total),0) as total from vwSoldItems where cast(sdate as date) BETWEEN CAST('" + date1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + date2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and  status like 'Sold' group by pcode,pdesc order by qty desc", con);
            }
            else if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                cmd = new MySqlCommand("select pcode, pdesc,ifnull(sum(qty),0) as qty, ifnull(sum(total),0) as total from vwSoldItems where cast(sdate as date) BETWEEN CAST('" + date1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + date2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and  status like 'Sold' group by pcode,pdesc order by total desc", con);
            }
            
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["Pcode"].ToString(), dr["pdesc"].ToString(), dr["qty"].ToString(),double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
            }
            dr.Close();
            con.Close();
        }

        public void CancelledOrders()
        {
            dataGridView5.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new MySqlCommand("select * from VWCancelledOrder where cast(sdate as date) BETWEEN CAST('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' AS DATE) ", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView5.Rows.Add(i, dr["transno"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["total"].ToString(), dr["sdate"].ToString(), dr["transno"].ToString(), dr["voidby"].ToString(), dr["cancelledby"].ToString(), dr["reason"].ToString(), dr["action"].ToString());

            }
            dr.Close();
            con.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
           
        }

        private void btnItemsLoad_Click(object sender, EventArgs e)
        {
       
        }
        public void LoadInventory()
        {
            int i = 0;
            dataGridView4.Rows.Clear();
            con.Open();
            cmd = new MySqlCommand("select p.pcode, p.barcode, p.pdesc, b.brand, c.category,p.price,p.qty,p.reorder from tblproduct as p INNER JOIN tblbrand as b on p.bid = b.id INNER JOIN  tblcategory as c on p.cid = c.id", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView4.Rows.Add(i, dr["pcode"].ToString(), dr["barcode"].ToString(), dr["pdesc"].ToString(), dr["brand"].ToString(), dr["category"].ToString(), dr["price"].ToString(), dr["reorder"].ToString(), dr["qty"].ToString());

            }
            dr.Close();
            con.Close();
        }
        public void LoadCriticalItems()
        {
            try
            {
                int i = 0;
                con.Open();
                dataGridView3.Rows.Clear();
                cmd= new MySqlCommand("SELECT * FROM `vwcriticalitems`",con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView3.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventoryReport frm = new frmInventoryReport();
            frm.LoadReport();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }
        public void LoadStockInHistory()
        {
            int i = 0;
            dataGridView6.Rows.Clear();
            con.Open();
            cmd = new MySqlCommand("select * from vwstock where cast(sdate as date) BETWEEN CAST('" + DateP1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + DateP2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and status like 'Done'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView6.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString());

            }
            dr.Close();
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventoryReport frm = new frmInventoryReport();
            if (cboTopSelect.Text == "SORT BY QTY")
            {
                //cmd = new MySqlCommand("select pcode, pdesc,ifnull(sum(qty),0) as qty, ifnull(sum(total),0) as total from vwSoldItems where cast(sdate as date) BETWEEN CAST('" + date1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + date2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and  status like 'Sold' group by pcode,pdesc order by qty desc", con);
                frm.LoadTopSelling("select pcode, pdesc,sum(qty) as qty, ifnull(sum(total),0) as total from vwSoldItems where cast(sdate as date) BETWEEN CAST('" + date1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + date2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and  status like 'Sold' group by pcode,pdesc order by qty desc", " From : " + date1.Value.ToString("yyyy-MM-dd") + " To : " + date2.Value.ToString("yyyy-MM-dd"), "TOP SELLING ITEMS SORTED BY QTY");
            }
            else if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                frm.LoadTopSelling("select pcode, pdesc,ifnull(sum(qty),0) as qty, ifnull(sum(total),0) as total from vwSoldItems where cast(sdate as date) BETWEEN CAST('" + date1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + date2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and  status like 'Sold' group by pcode,pdesc order by total desc", " From : " + date1.Value.ToString("yyyy-MM-dd") + " To : " + date2.Value.ToString("yyyy-MM-dd"), "TOP SELLING ITEMS SORTED BY TOTAL AMOUNT");
            }



            
           
            frm.ShowDialog();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventoryReport frm = new frmInventoryReport();
            frm.SoldItems("SELECT c.pcode,p.pdesc, c.price, sum(c.qty) AS tot_qty,sum(c.disc) as tot_disc, sum(c.total) as total from tblcart as c INNER JOIN tblproduct as p on c.pcode = p.pcode where status like 'Sold' and cast(sdate as date) BETWEEN CAST('" + datePicker1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + datePicker2.Value.ToString("yyyy-MM-dd") + "' AS DATE) GROUP BY c.pcode, p.pdesc, c.price", " From : " + datePicker1.Value.ToString("yyyy-MM-dd") + " To : " + datePicker2.Value.ToString("yyyy-MM-dd"));
            frm.ShowDialog();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cboTopSelect.Text == string.Empty)
            {
                MessageBox.Show("PLEASE SELECT FROM THE DROPDOWN LIST", "WARNING ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadRecord();
            LoadChartTopSell();
            
        }
        public void LoadChartTopSell()
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            con.Open();
            if (cboTopSelect.Text == "SORT BY QTY")
            {
                
                da = new MySqlDataAdapter("select pcode,ifnull(sum(qty),0) as qty from vwSoldItems where cast(sdate as date) BETWEEN CAST('" + date1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + date2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and  status like 'Sold' group by pcode order by qty desc", con);
            }
            else if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                 da = new MySqlDataAdapter("select pcode,ifnull(sum(total),0) as total from vwSoldItems where cast(sdate as date) BETWEEN CAST('" + date1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + date2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and  status like 'Sold' group by pcode order by total desc", con);
            }

            DataSet ds = new DataSet();
            da.Fill(ds, "TOPSELLING");
            chart1.DataSource = ds.Tables["TOPSELLING"];
            Series series = chart1.Series[0];
            series.ChartType = SeriesChartType.Doughnut;
            series.Name = "TOP SELLING";
            var chart = chart1;
            chart.Series[0].XValueMember = "pcode";
            if (cboTopSelect.Text == "SORT BY QTY") { chart.Series[0].YValueMembers = "qty"; }
           if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT") { chart.Series[0].YValueMembers = "total"; }
            chart.Series[0].IsValueShownAsLabel = true;
            if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT") { chart.Series[0].LabelFormat = "{#,##0.00}"; }
            if (cboTopSelect.Text == "SORT BY QTY") { chart.Series[0].LabelFormat = "{#,##0}"; }
            con.Close();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboTopSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                int i = 0;
                con.Open();
                cmd = new MySqlCommand("SELECT c.pcode,p.pdesc, c.price, sum(c.qty) AS tot_qty,sum(c.disc) as tot_disc, sum(c.total)as total from tblcart as c INNER JOIN tblproduct as p on c.pcode = p.pcode where status like 'Sold' and cast(sdate as date) BETWEEN CAST('" + datePicker1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + datePicker2.Value.ToString("yyyy-MM-dd") + "' AS DATE) GROUP BY c.pcode, p.pdesc, c.price", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView2.Rows.Add(i, dr["Pcode"].ToString(), dr["pdesc"].ToString(), Double.Parse(dr["price"].ToString()).ToString("#,##0.00"), dr["tot_qty"].ToString(), dr["tot_disc"].ToString(), Double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
                }
                dr.Close();
                con.Close();
                string x;

                con.Open();
                cmd = new MySqlCommand("SELECT sum(total) as total from tblcart where status like 'Sold' and cast(sdate as date) BETWEEN CAST('" + datePicker1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + datePicker2.Value.ToString("yyyy-MM-dd") + "' AS DATE) ", con);
                lblTotal.Text = Double.Parse(cmd.ExecuteScalar().ToString()).ToString("#,##0.00");
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmChart frm = new frmChart();
            frm.lblTitle.Text = "SOLD ITEMS [" + datePicker1.Value.ToShortDateString() + " - " + datePicker2.Value.ToShortDateString() + "]";
            frm.LoadChartSold("SELECT p.pdesc, sum(c.total)as total from tblcart as c INNER JOIN tblproduct as p on c.pcode = p.pcode where status like 'Sold' and cast(sdate as date) BETWEEN CAST('" + datePicker1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + datePicker2.Value.ToString("yyyy-MM-dd") + "' AS DATE) GROUP BY  p.pdesc order by total desc");
            frm.ShowDialog();
           
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
              frmInventoryReport frm = new frmInventoryReport();
              string param = "Date Convered: " + DateP1.Value.ToString("yyyy-MM-dd") + " - " + DateP2.Value.ToString("yyyy-MM-dd");
              frm.LoadStockInReport("select * from vwstock where cast(sdate as date) BETWEEN CAST('" + DateP1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + DateP2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and status like 'Done'", param);
            frm.ShowDialog();
            
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadStockInHistory();
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CancelledOrders();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
             
            frmInventoryReport f = new frmInventoryReport();
            string param ="Date Covered: " + dateTimePicker1.Value.ToString("yyyy-MM-dd") + " - " + dateTimePicker2.Value.ToString("yyyy-MM-dd");
            f.LoadCancelledOrderReport("select * from VWCancelledOrder where cast(sdate as date) BETWEEN CAST('" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' AS DATE)",param);
            f.ShowDialog();
        }
    }
}
