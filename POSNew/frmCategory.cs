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
    public partial class frmCategory : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        public frmCategory()
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            this.KeyPreview = true;
        }
        public void LoadCategory()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            con.Open();
            cmd = new MySqlCommand("SELECT * FROM tblCategory order by category", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i,dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CategoryList frm = new CategoryList(this);
            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false;
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoryList frm = new CategoryList(this);
                frm.IblCateID.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                frm.txtCategory.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.ShowDialog();


            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("ARE YOU SURE YOU WANT TO DELETE THIS CATEGORY?", "DELETE CATEGORY", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new MySqlCommand("delete from tblCategory where id like '" + dataGridView1[1, e.RowIndex].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("CATEGORY DELETED SUCCESSFULLY", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategory();
                }
            }
        }

        private void frmCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
