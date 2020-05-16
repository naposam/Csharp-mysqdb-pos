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
    public partial class frmLookup : Form
    {

        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
       // string stitle = "Simple Pos System";
        frmPos f;
        public frmLookup(frmPos frm)
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            f = frm;
            this.KeyPreview = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            con.Open();
            cmd = new MySqlCommand("SELECT p.pcode, p.barcode,p.pdesc, b.brand, c.category, p.price, p.qty from tblproduct as p inner join tblBrand as b on b.id = p.bid inner join tblCategory as c on c.id=p.cid where p.pdesc like '%" + txtSearch.Text + "%'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (ColName == "Select")
            {
                frmQty frm = new frmQty(f);
                frm.ProductDetails(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), Double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()), f.lbltransno.Text, int.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString()));
                dr.Close();
                con.Close();
                frm.ShowDialog();
            }
        }

        private void frmLookup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
