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
    public partial class frmStockin : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        string stitle = "SIMPLE POS SYSTEM";
        public frmStockin()
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            LoadVendor();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
       
        }

        public void LoadStockIn()
        {
            int i=0;
            dataGridView2.Rows.Clear();
            con.Open();
            cmd = new MySqlCommand("select * from vwstock where refno like '" + txtReferenceNo.Text + "' and status like 'Pending'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView2.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(),dr[8].ToString());
               
            }
            dr.Close();
            con.Close();

        }
        private void LoadStockInHistory()
        {
            int i=0;
            dataGridView1.Rows.Clear();
            con.Open();
            cmd = new MySqlCommand("select * from vwstock where cast(sdate as date) BETWEEN CAST('" + DateP1.Value.ToString("yyyy-MM-dd") + "' AS DATE) and CAST('" + DateP2.Value.ToString("yyyy-MM-dd") + "' AS DATE) and status like 'Done'", con); 
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), DateTime.Parse(dr[5].ToString()).ToShortDateString(),dr[6].ToString(),dr[8].ToString());
               
            }
            dr.Close();
            con.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView2.Columns[e.ColumnIndex].Name;
            if (ColName == "ColDelete")
            {
                if (MessageBox.Show("REMOVE THIS ITEM?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new MySqlCommand("delete from tblstock where id= '" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("ITEM DELETED", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStockIn();
                }
               
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSearchProductStockin frm = new frmSearchProductStockin(this);
            frm.loadProduct();
            frm.ShowDialog();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Clear()
        {
            txtReferenceNo.Clear();
            txtStockInBy.Clear();
            dtpStockInDate.Value = DateTime.Now;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    if (MessageBox.Show("DO YOU WANT TO SAVE THIS RECORDS", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { 
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        con.Open();
                        cmd = new MySqlCommand("update tblproduct set qty = qty +" + int.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString()) + " where pcode like '" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "'",con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        con.Open();
                        cmd = new MySqlCommand("update tblstock set qty = qty + " + int.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString()) + ", status='Done' where id like '" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                    Clear();
                    LoadStockIn();
                

                }

                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadStockInHistory();
        }

        private void cboVendor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        public void LoadVendor(){
            cboVendor.Items.Clear();
            con.Open();
            cmd = new MySqlCommand("select * from tblvendor", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboVendor.Items.Add(dr["vendor"].ToString());
            }
            dr.Close();
            con.Close();


    }

        private void cboVendor_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new MySqlCommand("select * from tblvendor where vendor like '" + cboVendor.Text + "'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblVendorID.Text = dr["id"].ToString();
                txtPerson.Text = dr["contactperson"].ToString();
                txtAddress.Text = dr["address"].ToString();
            }
            dr.Close();
            con.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random rmd = new Random();
            txtReferenceNo.Clear();
            //int i=0;
            //for(i=0;i<10;i++){
                txtReferenceNo.Text += rmd.Next();
            //}
            
        }
    }
}
