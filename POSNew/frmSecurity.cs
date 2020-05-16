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
    public partial class frmSecurity : Form
    {

        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        public string _password,_UserName="";
        public bool _isactive = false;
        public frmSecurity()
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            txtUsername.Focus();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string  _role="", _name = "";
            try
            {
                bool found = false;
                con.Open();
                cmd = new MySqlCommand("select * from tblUser where username=@username and password=@password", con);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    _UserName = dr["username"].ToString();
                    _role = dr["role"].ToString();
                    _name = dr["name"].ToString();
                    _password = dr["password"].ToString();
                    _isactive = bool.Parse(dr["isactive"].ToString());


                }
                else
                {
                    found = false;
                }
                dr.Close();
                con.Close();
                if (found == true)
                {
                    if (_isactive == false)
                    {
                        MessageBox.Show("ACCOUNT IS DEACTIVATE, UNABLE TO LOGIN", "DEACTIVETED ACCOUNT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (_role == "Cashier")
                    {
                        MessageBox.Show("WELCOME " + _name + " ! ", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Clear();
                        txtUsername.Clear();
                        this.Hide();
                        frmPos frm = new frmPos(this);
                        frm.lblUsername.Text = _UserName.ToString();
                        frm.lblName.Text = _name + " | " + _UserName;
                        frm.ShowDialog();


                    }
                    else
                    {
                        MessageBox.Show("WELCOME " + _name + " ! ", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Clear();
                        txtUsername.Clear();
                        this.Hide();
                        Form1 frm = new Form1();
                        frm.lblName.Text = _name;
                        frm.lblRole.Text = _role;
                        frm.lblUser.Text = _UserName;
                        frm._password = _password;
                        frm._user = _UserName;
                        
                        frm.ShowDialog();
                        
                    }
                }
                else
                
                {
                    MessageBox.Show("INVALID USERNAME OR PASSWORD", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("EXIT APPLICATION?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
