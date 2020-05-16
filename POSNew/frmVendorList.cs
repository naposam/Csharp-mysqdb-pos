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
    public partial class frmVendorList : Form
    {


        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        public frmVendorList()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = dbCon.getConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmVendor frm = new frmVendor(this);
            frm.btnUpdate.Enabled = false;
            frm.btnSave.Enabled = true;
            frm.ShowDialog();
        }
        public void LoadRecords()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new MySqlCommand("select * from tblvendor", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());

            }
            dr.Close();
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (ColName == "Edit")
            {
                frmVendor f = new frmVendor(this);
                f.lblID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.txtVendor.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                f.txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.txtContact.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.txtTel.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                f.txtFax.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                f.btnSave.Enabled = false;
                f.btnUpdate.Enabled = true;
                f.ShowDialog();


            }
            else if (ColName == "Delete")
            {
                if (MessageBox.Show("DELETE THIS RECORDS? CLICK YES TO CONFIRM", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new MySqlCommand("delete from tblvendor where id like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("RECORDS DELETED SUCCESSFULLY", "DELETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRecords();
                }
            }
        }
    }
}
