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
    public partial class frmVendor : Form
    {

        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        frmVendorList f;
        public frmVendor(frmVendorList f)
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            this.f = f;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
        try{
            if(MessageBox.Show("ADD THIS RECORDS?","POS",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes){
            con.Open();
            cmd= new MySqlCommand("INSERT INTO tblvendor(vendor,address,contactperson,telephone ,email,fax)Values(@vendor,@address,@contactperson,@telephone ,@email,@fax)",con);
            cmd.Parameters.AddWithValue("@vendor",txtVendor.Text);
            cmd.Parameters.AddWithValue("@address",txtAddress.Text);
            cmd.Parameters.AddWithValue("@contactperson",txtContact.Text);
            cmd.Parameters.AddWithValue("@telephone",txtTel.Text);
            cmd.Parameters.AddWithValue("@email",txtEmail.Text);
            cmd.Parameters.AddWithValue("@fax",txtFax.Text);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("RECORDS ADDED","POS",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Clear();
            f.LoadRecords();
        }
        }catch(Exception ex){
            MessageBox.Show(ex.Message,"WARNING",MessageBoxButtons.OK,MessageBoxIcon.Warning);

        }
        }

        public void Clear()
        {
            txtFax.Clear();
            txtVendor.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
            txtTel.Clear();
            txtContact.Clear();
            txtVendor.Focus();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("UPDATE THIS RECORDS??", "POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new MySqlCommand("update tblvendor set vendor=@vendor,address=@address,contactperson=@contactperson,telephone=@telephone ,email=@email,fax=@fax where id=@id ", con);
                    cmd.Parameters.AddWithValue("@vendor", txtVendor.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@contactperson", txtContact.Text);
                    cmd.Parameters.AddWithValue("@telephone", txtTel.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@fax", txtFax.Text);
                    cmd.Parameters.AddWithValue("@id", lblID.Text);
                    cmd.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("RECORDS HAS BEEN UPDATED SUCCESSFULLY", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.LoadRecords();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
