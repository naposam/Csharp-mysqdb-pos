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
    public partial class frmQty : Form
    {

        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        private string pcode;
        private double price;
        private string transno;
        private int  qty;
        string stitle = "Simple Pos System";
        frmPos fpos;
        public frmQty(frmPos frmpos)
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            fpos = frmpos;
        }
        public void ProductDetails(string pcode, double price, string transno, int qty)
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
            this.qty = qty;
        }
        private void frmQty_Load(object sender, EventArgs e)
        {

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar==13) && (txtQty.Text != string.Empty))
            {
                string id="";
                int cart_qty=0;
                bool found = false;
               
                con.Open();
                cmd = new MySqlCommand("select * from tblcart where transno = @transno and pcode = @pcode", con);
                cmd.Parameters.AddWithValue("@transno",fpos.lbltransno.Text);
                cmd.Parameters.AddWithValue("@pcode",pcode);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                   
                    id = dr["id"].ToString();
                    cart_qty = int.Parse(dr["qty"].ToString());
                }
                else
                {
                    found = false;
                }
                dr.Close();
                con.Close();
                if (found == true)
                {
                    if (qty < (int.Parse(txtQty.Text) + cart_qty))
                    {
                        MessageBox.Show("UNABLE TO PROCEED. REMAINING QTY ON HAND IS " + qty, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    con.Open();
                    cmd = new MySqlCommand("update tblcart set qty = (qty + "+int.Parse(txtQty.Text) + ") where id = '" + id + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.LoadCart();
                    this.Dispose();
                }
                else
                {

                    if (qty < int.Parse(txtQty.Text))
                    {
                        MessageBox.Show("UNABLE TO PROCEED. REMAINING QTY ON HAND IS " + qty, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    con.Open();
                    cmd = new MySqlCommand("insert into tblcart(transno,pcode,price,qty,sdate,cashier) VALUES(@transno,@pcode,@price,@qty,@sdate,@cashier)", con);
                    cmd.Parameters.AddWithValue("@transno", transno);
                    cmd.Parameters.AddWithValue("@pcode", pcode);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                    cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@cashier", fpos.lblUsername.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.LoadCart();
                    this.Dispose();
                }

            }
        }
    }
}
