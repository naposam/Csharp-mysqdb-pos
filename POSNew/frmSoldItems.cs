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
namespace POSNew
{
    public partial class frmSoldItems : Form
    {

        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        public string suser;
        public frmSoldItems()
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            dt1.Value = DateTime.Now;
            dt2.Value = DateTime.Now;
            LoadRecord();
            LoadCashier();
            this.KeyPreview = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadRecord()
        {
            int i = 0;
            double _total = 0;
            dataGridView1.Rows.Clear();
            con.Open();
            if (cboCashier.Text == "All Cashiers") { cmd = new MySqlCommand("SELECT c.id, c.transno, c.pcode,p.pdesc, c.price,c.qty,c.disc,c.total from tblcart as c INNER JOIN tblproduct as p on c.pcode = p.pcode where status like 'Sold' and cast(sdate as date) BETWEEN CAST('" + dt1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + dt2.Value.ToString("yyyy-MM-dd") + "' AS DATE)", con); }
            else { cmd = new MySqlCommand("SELECT c.id, c.transno, c.pcode,p.pdesc, c.price,c.qty,c.disc,c.total from tblcart as c INNER JOIN tblproduct as p on c.pcode = p.pcode where status like 'Sold' and cast(sdate as date) BETWEEN CAST('" + dt1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + dt2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and cashier like'" + cboCashier.Text + "' ", con); }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                _total += Double.Parse(dr["total"].ToString());
                dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["transno"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), dr["total"].ToString());

            }
            dr.Close();
            con.Close();
            lblTotal.Text = _total.ToString("#,##0.00");
        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReportSold frm = new frmReportSold(this);
            frm.LoadReport();
            frm.ShowDialog();
        }

        private void cboCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        public void LoadCashier()
        {
            cboCashier.Items.Clear();
            cboCashier.Items.Add("All Cashiers");
            con.Open();
            cmd = new MySqlCommand("select * from tbluser where role like 'Cashier'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboCashier.Items.Add(dr["username"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void cboCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "colCancel")
            {
                frmCancelDetails f= new frmCancelDetails(this);
                f.txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.txtTransNo.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                f.txtPcode.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.txtQty.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                f.txtDiscount.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                f.txtTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                f.txtCancelby.Text = suser;
                f.ShowDialog();
            }
        }

        private void frmSoldItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void frmSoldItems_Load(object sender, EventArgs e)
        {
            label2.Text = frmStore._storeName;
        }
    }
}
