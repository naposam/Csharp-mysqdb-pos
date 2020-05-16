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
    public partial class frmProduct : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        DbConnection dbCon = new DbConnection();
        frmProductList flist;
        
        public frmProduct(frmProductList frm)
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            flist = frm;
            ProductCodeLoad();
        }
        public void ProductCodeLoad()
        {
            Random r = new Random();
            int genRand = r.Next(10, 50);
            txtPcode.Text += "P00" + genRand.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadCategory()
        {
            cboCategory.Items.Clear();
            con.Open();
            cmd = new MySqlCommand("select category from tblCategory", con);
            dr = cmd.ExecuteReader();
            while(dr.Read()){
                cboCategory.Items.Add(dr[0].ToString());
            }
            con.Close();
        }

        public void LoadBrand()
        {
            cboBrand.Items.Clear();
            con.Open();
            cmd = new MySqlCommand("select brand from tblbrand", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboBrand.Items.Add(dr[0].ToString());
            }
            con.Close();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ARE YOU SURE YOU WANT TO ADD THIS PRODUCT?", "ADD PRODUCT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid=""; string cid = "";
                    con.Open();
                    cmd = new MySqlCommand("Select id from tblBrand where brand like '" + cboBrand.Text + "'", con);
                    dr= cmd.ExecuteReader();
                    dr.Read();
                    if(dr.HasRows){bid= dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();

                    con.Open();
                    cmd = new MySqlCommand("Select id from tblCategory where category like '" + cboCategory.Text + "'", con);
                    dr= cmd.ExecuteReader();
                    dr.Read();
                    if(dr.HasRows){cid= dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();


                    con.Open();
                    cmd = new MySqlCommand("INSERT INTO tblproduct(pcode,barcode,pdesc,bid,cid,price,reorder)VALUES(@pcode,@barcode,@pdesc,@bid,@cid,@price,@reorder)", con);
                    cmd.Parameters.AddWithValue("@pcode",txtPcode.Text);
                    cmd.Parameters.AddWithValue("@barcode",txtBarcode.Text);
                    cmd.Parameters.AddWithValue("@pdesc", txtPDesc.Text);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@price", Double.Parse(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@reorder", int.Parse(txtReorder.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("PRODUCT ADDED SUCCESSFULLY");
                    Clear();
                    flist.LoadRecords();
                    ProductCodeLoad();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtPrice.Clear();
            txtPDesc.Clear();
            txtPcode.Clear();
            txtBarcode.Clear();
            cboBrand.Text = "";
            cboCategory.Text = "";
            txtPcode.Focus();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ARE YOU SURE YOU WANT TO UPDATE THIS PRODUCT?", "UPDATE PRODUCT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = ""; string cid = "";
                    con.Open();
                    cmd = new MySqlCommand("Select id from tblbrand where brand like '" + cboBrand.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        bid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();

                    con.Open();
                    cmd = new MySqlCommand("Select id from tblCategory where category like '" + cboCategory.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        cid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();


                    con.Open();
                    cmd = new MySqlCommand("UPDATE tblproduct SET barcode=@barcode,pdesc=@pdesc,bid=@bid,cid=@cid,price=@price, reorder=@reorder WHERE pcode like @pcode", con);
                    cmd.Parameters.AddWithValue("@pcode", txtPcode.Text);
                    cmd.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cmd.Parameters.AddWithValue("@pdesc", txtPDesc.Text);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@price",Double.Parse( txtPrice.Text));
                    cmd.Parameters.AddWithValue("@reorder", int.Parse(txtReorder.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("PRODUCT UPDATED SUCCESSFULLY");
                    Clear();
                    flist.LoadRecords();
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
            this.Dispose();
           // txtPcode.Enabled = true;
           // cboBrand.Enabled = true;
           // cboCategory.Enabled = false;
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46)
            {

            }else if(e.KeyChar==8){

            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))
            {
                e.Handled = true;
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                Zen.Barcode.CodeQrBarcodeDraw qr = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                pictureBox1.Image = qr.Draw(txtBarcode.Text, 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
