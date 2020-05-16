﻿using System;
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
    public partial class frmAdjustment : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        Form1 f;
        int _qty = 0;
        public frmAdjustment(Form1 f)
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = dbCon.getConnection();
            this.f = f;
            this.KeyPreview = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void ReferenceNo()
        {
            Random rnd = new Random();
            txtRef.Text = rnd.Next().ToString();
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

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                LoadRecords();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string Colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (Colname == "Select")
            {
                txtPcode.Text= dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDesc.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                _qty = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtQty.Text) > _qty)
                {
                    MessageBox.Show("STOCK QUANTITY ON HAND SHOULD BE GREATER THAN ADJUSTMENT QTY.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //UPDATING STOCK
                if (cboCommand.Text == "REMOVE FROM INVENTORY")
                {
                    SqlStatement("update tblproduct set qty = (qty - " + int.Parse(txtQty.Text) + ") where pcode like '" + txtPcode.Text + "'");
                }
                else if (cboCommand.Text == "ADD TO INVENTORY")
                {
                    SqlStatement("update tblproduct set qty = (qty + " + int.Parse(txtQty.Text) + ") where pcode like '" + txtPcode.Text + "'");  
                }

                SqlStatement("insert into tbladjustment(referenceno, pcode, qty, action, remarks, sdate,user)values('" + txtRef.Text + "', '" + txtPcode.Text + "', '" + txtQty.Text+ "','" + cboCommand.Text + "','" + txtRemarks.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + txtUser.Text + "')");

                MessageBox.Show("STOCK HAS BEEN SUCCESSFULLY ADJUSTED", "PROCESS COMPLTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRecords();
                Clear();
                
            }catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Clear()
        {
            txtDesc.Clear();
            txtPcode.Clear();
            txtQty.Clear();
            txtRef.Clear();
            txtRemarks.Clear();
            cboCommand.Text = "";
            ReferenceNo();
        }

        public void SqlStatement(string _sql)
        {
            con.Open();
            cmd = new MySqlCommand(_sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
                LoadRecords();
            
        }

        private void frmAdjustment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
