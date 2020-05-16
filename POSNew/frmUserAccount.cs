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
    public partial class frmUserAccount : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        Form1 f;
        public frmUserAccount(Form1 f)
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            this.f = f;
        }

        private void frmUserAccount_Resize(object sender, EventArgs e)
        {
            metroTabControl1.Left = (this.Width - metroTabControl1.Width) / 2;
            metroTabControl1.Top = (this.Height - metroTabControl1.Height) / 2;



        }
        private void Clear()
        {
            txtName.Clear();
            txtPassword.Clear();
            txtRetypepass.Clear();
            txtUsername.Clear();
            cboRole.Text = "";
            txtUsername.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != txtRetypepass.Text)
                {
                    MessageBox.Show("PASSWORD DO NOT MATCH","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                con.Open();
                cmd = new MySqlCommand("insert into tbluser(username,password,role,name)values(@username,@password,@role,@name)", con);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@role", cboRole.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("NEW ACCOUNT HAS SAVED");
                Clear();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtOld1.Text != f._password){
                    MessageBox.Show("OLD PASSSWORD DID NOT MATCH", "INVALID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtNew1.Text != txtConfirm1.Text)
                {
                    MessageBox.Show("NEW PASSSWORDS Do NOT MATCH", "INVALID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                con.Open();
                cmd = new MySqlCommand("update tbluser set password=@password where username=@username", con);
                cmd.Parameters.AddWithValue("@password", txtNew1.Text);
                cmd.Parameters.AddWithValue("@username", txtUser1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("PASSWORD CHANGED SUCCESSFULLY", "PASSWORD CHANGED", MessageBoxButtons.OK, MessageBoxIcon.Information);
               // txtUser1.Clear();
                txtConfirm1.Clear();
                txtNew1.Clear();
                txtOld1.Clear();
                
            }
            catch (Exception ex)
            {
                Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtUser2_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
                con.Open();
                cmd = new MySqlCommand("select * from tbluser where username=@username", con);
                cmd.Parameters.AddWithValue("@username", txtUser2.Text);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    checkBox1.Checked = bool.Parse(dr["isactive"].ToString());
                }
                else
                {
                    checkBox1.Checked = false;
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                bool found = true;
                con.Open();
                cmd = new MySqlCommand("select * from tbluser where username=@username", con);
                cmd.Parameters.AddWithValue("@username", txtUser2.Text);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows){found = true;} else{found = false;}
                dr.Close();
                con.Close();

                if (found == true) { 
                con.Open();
                cmd = new MySqlCommand("update tbluser set isactive=@isactive where username=@username", con);
                cmd.Parameters.AddWithValue("@isactive", checkBox1.Checked.ToString());
                cmd.Parameters.AddWithValue("@username", txtUser2.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ACCOUNT STATUS HAS BEEN SUCCESSFULLY UPDATED", "UPDATED STATUS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUser2.Clear();
                checkBox1.Checked = false;
                }
                else
                {
                    MessageBox.Show("ACCOUNT DOES NOT EXISTS", "INVALID ACCOUNT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}