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
using Tulpep.NotificationWindow;
namespace POSNew
{
    public partial class Form1 : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        public string _password, _user;
       
        public Form1()
        {
            InitializeComponent();
           
            try
            {
                con = new MySqlConnection(dbCon.getConnection());
               // con.Open();
                //MessageBox.Show("Connected");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MyDashboard();
            NotifyCriticalItems();

            
            
            
        }

        public void NotifyCriticalItems()
        {
            string critical = "";

            con.Open();
            cmd = new MySqlCommand("select count(*) from vwCriticalItems", con);
            string count = cmd.ExecuteScalar().ToString();
            con.Close();


            int i = 0;
            con.Open();
            cmd = new MySqlCommand("select * from vwCriticalItems", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                critical += i + ". " + dr["pdesc"].ToString() + Environment.NewLine;
            }
            dr.Close();
            con.Close();

            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.close2;
            popup.TitleText = count + " CRITICAL ITEM(S) ";
            popup.ContentText = critical;
            popup.Popup();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            frmBrand frm = new frmBrand();
            frm.TopLevel = false;
            panelShow.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            button10.ForeColor = Color.White;
            button10.BackColor = Color.Red;
        }

        private void button10_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void button10_MouseLeave(object sender, EventArgs e)
        {
            button10.BackColor = Color.Transparent;
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory();
            frm.TopLevel = false;
            panelShow.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadCategory();
           
            frm.Show();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProductList frm = new frmProductList();
            frm.TopLevel = false;
            panelShow.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadRecords();
            frm.Show();
        }



        private void panelShow_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            frmStockin frm = new frmStockin();
           frm.TopLevel = false;
            panelShow.Controls.Add(frm);
            frm.BringToFront();
           /// frm.loadProduct();
            frm.Show();
            

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("LOGOUT APPLICATION?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                frmSecurity frm = new frmSecurity();
                frm.ShowDialog();
            }
            
        }

        private void panelDashbaord_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            frmSoldItems frm = new frmSoldItems();
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmUserAccount frm = new frmUserAccount(this);
            frm.TopLevel= false;
            panelShow.Controls.Add(frm);
            frm.txtUser1.Text = _user;
            frm.BringToFront();
            frm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmRecords frm = new frmRecords();
            frm.TopLevel = false;
            frm.LoadCriticalItems();
            frm.LoadInventory();
            frm.CancelledOrders();
            frm.LoadStockInHistory();
            panelShow.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmStore frm = new frmStore();
            frm.TopLevel = false;
            frm.LoadRecords();
            panelShow.Controls.Add(frm);
            frm.BringToFront();

            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyDashboard();
        }

        public void MyDashboard()
        {
            frmDashBoard frm = new frmDashBoard();
            frm.TopLevel = false;
            panelShow.Controls.Add(frm);
            frm.lblDailySales.Text = dbCon.DailySales().ToString("#,##0.00");
            frm.lblProductLine.Text = dbCon.ProductLine().ToString("#,##0");
            frm.lblQty.Text = dbCon.StockOnHand().ToString("#,##0");
            frm.lblCritical.Text = dbCon.CrititalItems().ToString("#,##0");
            frm.BringToFront();

            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmVendorList frm = new frmVendorList();
            frm.TopLevel = false;
            frm.LoadRecords();
            panelShow.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnAdjustment_Click(object sender, EventArgs e)
        {
            frmAdjustment frm = new frmAdjustment(this);
            frm.TopLevel = false;
            frm.LoadRecords();
            frm.txtUser.Text = lblUser.Text;
            frm.ReferenceNo();
            panelShow.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MyDashboard();
        }
    }
}
