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
    public partial class CategoryList : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        frmCategory flist;
        public CategoryList(frmCategory frm)
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            flist = frm;
            this.KeyPreview = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtCategory.Clear();
            txtCategory.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCategory.Text== "")
            {
                MessageBox.Show("ENTER CATEGORY NAME");
                txtCategory.Focus();
                return;
            }
            else
            {

                try
                {
                    if (MessageBox.Show("DO YOU WANT TO SAVE THIS CATEGORY", "ADD CATEGORY", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new MySqlCommand("INSERT INTO `tblCategory`(category) VALUES(@category)", con);
                        cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("RECORDS SAVED SUCCESSFULLY");
                        Clear();
                        flist.LoadCategory();

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You sure you want to update this brand ", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new MySqlCommand("update tblCategory set category = @category where id like '" + IblCateID.Text + "'", con);
                    cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("category Updated");
                    Clear();
                    flist.LoadCategory();
                    this.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void CategoryList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
