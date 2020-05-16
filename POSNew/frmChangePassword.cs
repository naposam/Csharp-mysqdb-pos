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
    public partial class frmChangePassword : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        frmPos f;
        public frmChangePassword(frmPos frm)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string _oldPassword = dbCon.GetPassword(f.lblUsername.Text);
                if (_oldPassword != txtOldPassword.Text)
                {
                    MessageBox.Show("OLD PASSWORD DO NOT MATCH!","WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtNewPassword.Text != txtConfirmNewPassword.Text)
                {
                    MessageBox.Show("CONFIRM NEW PASSWORD DO NOT MATCH!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("CHANGE PASSWORD?", "COMFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new MySqlCommand("update tbluser set password =@password where username=@username", con);
                        cmd.Parameters.AddWithValue("@password", txtNewPassword.Text);
                        cmd.Parameters.AddWithValue("@username", f.lblUsername.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("PASSWORD CHANGED SUCCESSFULLY", "SAVED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }

                }
                
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panelDashbaord_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmChangePassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
