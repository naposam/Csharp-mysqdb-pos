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
    public partial class frmBrand : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DbConnection dbCon = new DbConnection();
        public frmBrand()
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            LoadRcords();
            this.KeyPreview = true;
            //for (int i = 1; i <= 10; ++i)
           // {
              //  dataGridView1.Rows.Add(i, " 1 ", " Brand "+ i);
           // }
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                BrandList frm = new BrandList(this);
                frm.lblID.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                frm.textBox1.Text = dataGridView1[2,e.RowIndex].Value.ToString();
                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.ShowDialog();


            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("ARE YOU SURE YOU WANT TO DELETE THIS BRAND?", "DELETE BRAND", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new MySqlCommand("delete from tblbrand where id like '" + dataGridView1[1,e.RowIndex].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("BRAND DELETED SUCCESSFULLY","POS",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    LoadRcords();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BrandList frmbrand = new BrandList(this);
            frmbrand.btnUpdate.Enabled = false;
            frmbrand.btnSave.Enabled = true;
            frmbrand.ShowDialog();
            
        }

        public void LoadRcords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            con.Open();
            cmd = new MySqlCommand("Select * from tblbrand order by brand", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString());
            }
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

    }
}
