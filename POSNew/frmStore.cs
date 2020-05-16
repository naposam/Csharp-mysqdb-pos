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
    public partial class frmStore : Form
    {

        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        public static string _storeName,_storeAddress;
        
        public frmStore()
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            LoadRecords();
           
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }
        public void  LoadRecords()
        {
            con.Open();
            cmd = new MySqlCommand("select * from tblstore", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                txtStoreName.Text = dr["store"].ToString();
                txtAddress.Text = dr["address"].ToString();
                _storeName = dr["store"].ToString();
                _storeAddress = dr["address"].ToString();
                
            }
            else
            {
                txtAddress.Clear();
                txtStoreName.Clear();
            }
            dr.Close();
            con.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("SAVE STORE DETAILS?","SAVE STORE DETAILS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int count;
                    con.Open();
                    cmd = new MySqlCommand("select count(*) from tblstore", con);
                    count = int.Parse(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if (count > 0)
                    {
                        con.Open();
                        cmd = new MySqlCommand("update tblstore set store=@store , address=@address", con);
                        cmd.Parameters.AddWithValue("@store", txtStoreName.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.ExecuteNonQuery();

                        con.Close();
                    }
                    else
                    {
                        con.Open();
                        cmd = new MySqlCommand("insert into tblstore(store,address)values(@store,@address)", con);
                        cmd.Parameters.AddWithValue("@store", txtStoreName.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.ExecuteNonQuery();

                        
                    }
                    MessageBox.Show("STORE DETAILS SAVED", "SAVED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message,"WARNING",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

    }
}
