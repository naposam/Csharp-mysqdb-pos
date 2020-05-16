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
using Microsoft.Reporting.WinForms;

namespace POSNew
{
    public partial class frmReportSold : Form
    {

        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        frmSoldItems f;
        public string StoreName, StoreAddress;
        public frmReportSold(frmSoldItems frm)
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());

            f = frm;
        }

        private void frmReportSold_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            StoreName = frmStore._storeName;
            StoreAddress = frmStore._storeAddress;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadReport()
        {
            try
            {
                ReportDataSource rptDS;
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptSoldItems.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();
                DataSet1 ds = new DataSet1();
                MySqlDataAdapter da = new MySqlDataAdapter();
                con.Open();
                if (f.cboCashier.Text == "All Cashiers") { da.SelectCommand = new MySqlCommand("SELECT c.id, c.transno, c.pcode,p.pdesc, c.price,c.qty,c.disc,c.total from tblcart as c INNER JOIN tblproduct as p on c.pcode = p.pcode where status like 'Sold' and cast(sdate as date) BETWEEN CAST('" + f.dt1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + f.dt2.Value.ToString("yyyy-MM-dd") + "' AS DATE)", con); }
                else { da.SelectCommand = new MySqlCommand("SELECT c.id, c.transno, c.pcode,p.pdesc, c.price,c.qty,c.disc,c.total from tblcart as c INNER JOIN tblproduct as p on c.pcode = p.pcode where status like 'Sold' and cast(sdate as date) BETWEEN CAST('" + f.dt1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + f.dt2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and cashier like '" + f.cboCashier.Text + "'", con); }
                da.Fill(ds.Tables["dtSoldReport"]);
                con.Close();
                ReportParameter pStoreName = new ReportParameter("pStoreName","Napoleon Sam Enterprise");
                ReportParameter pStoreAddress = new ReportParameter("pStoreAddress","PLT 457 ANAJI TAKORADI");
                ReportParameter pDate = new ReportParameter("pDate","Date From: "+ f.dt1.Value.ToShortDateString() + " To : " + f.dt2.Value.ToShortDateString());
                ReportParameter pCashier = new ReportParameter("pCashier", "Cashier " + f.cboCashier.Text);
                ReportParameter pHeader = new ReportParameter("pHeader", "SALES REPORT");

                reportViewer1.LocalReport.SetParameters(pStoreName);
                reportViewer1.LocalReport.SetParameters(pStoreAddress);
                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pCashier);
                reportViewer1.LocalReport.SetParameters(pHeader);
                


                rptDS=new ReportDataSource("DataSet1", ds.Tables["dtSoldReport"]);
                reportViewer1.LocalReport.DataSources.Add(rptDS);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void panelDashbaord_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
