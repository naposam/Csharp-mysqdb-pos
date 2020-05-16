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
    public partial class BrandList : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        frmBrand frmlist;
        public BrandList(frmBrand flist)
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            frmlist = flist;
            this.KeyPreview = true;
           
        }

        private void BrandList_Load(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            textBox1.Clear();
            textBox1.Focus();


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textBox1.Text =="")
            {
                MessageBox.Show("ENTER BRAND NAME");
                textBox1.Focus();
                return;
            }
            else { 

            try
            {
                if (MessageBox.Show("DO YOU WANT TO SAVED THIS BRAND", "Add Brand", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new MySqlCommand("INSERT INTO `tblbrand`(brand) VALUES(@brand)",con);
                    cmd.Parameters.AddWithValue("@brand", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("RECORDS SAVED SUCCESSFULLY");
                    Clear();
                    frmlist.LoadRcords();

                }

            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You sure you want to update this brand ", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new MySqlCommand("update tblbrand set brand = @brand where id like '" + lblID.Text + "'",con);
                    cmd.Parameters.AddWithValue("@brand", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("BRAND UPDATED");
                    Clear();
                    frmlist.LoadRcords();
                    this.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BrandList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
