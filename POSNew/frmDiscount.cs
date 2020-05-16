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
    public partial class frmDiscount : Form
    {

        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        string stitle = "SIMPLE POS SYSTEM";
        frmPos f;
        public frmDiscount(frmPos frm)
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

        private void txtPercentage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double discount = Double.Parse(txtPrice.Text) * Double.Parse(txtPercentage.Text);
                txtAmount.Text = discount.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                txtAmount.Text = "0.00";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ADD DISCOUNT?,CLICK YES TO CONFIRM", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new MySqlCommand("update tblcart set disc = @disc, disc_percent = @disc_percent where id=@id", con);
                    cmd.Parameters.AddWithValue("@disc", Double.Parse(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@disc_percent", Double.Parse(txtPercentage.Text));
                    cmd.Parameters.AddWithValue("@id", int.Parse(lblID.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    f.LoadCart();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void frmDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
