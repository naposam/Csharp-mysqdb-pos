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
    public partial class frmSearchProductStockin : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;

        string stitle = "SIMPLE POS SYSTEM";
        frmStockin slist;
        public frmSearchProductStockin(frmStockin flist)
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            slist = flist;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void loadProduct()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            con.Open();
            cmd = new MySqlCommand("SELECT pcode, pdesc, qty FROM tblproduct where pdesc like '%" + txtSearch.Text + "%' order by pdesc", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "colSelect")
            {

                if (slist.txtReferenceNo.Text == string.Empty) { MessageBox.Show("Please enter reference number no", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning); slist.txtReferenceNo.Focus(); return; }
                if (slist.txtStockInBy.Text == string.Empty) { MessageBox.Show("Please enter stock in", stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning); slist.txtStockInBy.Focus(); return; }
                if (MessageBox.Show("ADD THIS ITEM?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new MySqlCommand("INSERT INTO tblstock(refno,pcode,sdate,stockinby,vendorid)VALUES(@refno,@pcode,@sdate,@stockinby,@vendorid)", con);
                    //where pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.Parameters.AddWithValue("@refno", slist.txtReferenceNo.Text);
                    cmd.Parameters.AddWithValue("@pcode", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@sdate", slist.dtpStockInDate.Value);
                    cmd.Parameters.AddWithValue("@stockinby", slist.txtStockInBy.Text);
                    cmd.Parameters.AddWithValue("@vendorid", slist.lblVendorID.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("ADDED ", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                   slist.LoadStockIn();
                }



            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadProduct();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
