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
    public partial class frmVoid : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        frmCancelDetails f;
        public frmVoid(frmCancelDetails frm)
        {

            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            f = frm;
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtPassword.Text != string.Empty))
                {
                    string user="";
                    con.Open();
                    cmd = new MySqlCommand("select * from tbluser where username = @username and password = @password", con);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        user = dr["username"].ToString();
                        dr.Close();
                        con.Close();
                       
                        

                        SaveCancelOrder(user);
                        if (f.cboAction.Text == "Yes")
                        {
                            UpdateData("update tblproduct set qty = qty + " + int.Parse(f.txtCancelQty.Text) + " where pcode = '" + f.txtPcode.Text + "'");
                        }
                        UpdateData("update tblcart set qty = qty - " + int.Parse(f.txtCancelQty.Text) + " where id like '" + f.txtID.Text + "'");
                        UpdateData("delete from tblcart where qty=0 and id like '" + f.txtID.Text + "'");
                        MessageBox.Show("ORDER TRANSACTION SUCCESSFULLY CANCELLED", "CANCELLED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                        f.RefreshList();
                        f.Dispose();
                    }
                    dr.Close();
                    con.Close();
                    
                }
               
               
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void SaveCancelOrder(string user)
        {
            con.Open();
            cmd = new MySqlCommand("insert into tblCancel(transno,pcode,price,qty,sdate,voidby,cancelledby,reason,action)values(@transno,@pcode,@price,@qty,@sdate,@voidby,@cancelledby,@reason,@action)", con);
            cmd.Parameters.AddWithValue("@transno", f.txtTransNo.Text);
            cmd.Parameters.AddWithValue("@pcode", f.txtPcode.Text);
            cmd.Parameters.AddWithValue("@price", Double.Parse(f.txtPrice.Text));
            cmd.Parameters.AddWithValue("@qty", int.Parse(f.txtQty.Text));
            cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
            cmd.Parameters.AddWithValue("@voidby", user);
            cmd.Parameters.AddWithValue("@cancelledby", f.txtCancelby.Text);
            cmd.Parameters.AddWithValue("@reason", f.txtReason.Text);
            cmd.Parameters.AddWithValue("@action", f.cboAction.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void UpdateData(string sql)
        {
            con.Open();
            cmd = new MySqlCommand(sql,con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
