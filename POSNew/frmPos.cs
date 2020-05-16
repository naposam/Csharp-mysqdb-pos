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
using Tulpep.NotificationWindow;

namespace POSNew
{
    public partial class frmPos : Form
    {
        string id;
        string price;
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        string stitle = "Simple Pos System";
        int qty;
        frmSecurity f;
        public frmPos(frmSecurity frm)
        {
            InitializeComponent();
            lblDate.Text = DateTime.Now.ToLongDateString();
            con = new MySqlConnection(dbCon.getConnection());
            this.KeyPreview = true;
            f = frm;
            NotifyCriticalItems();
        }

        public void NotifyCriticalItems()
        {
            string critical = "";

            con.Open();
            cmd = new MySqlCommand("select count(*) from vwCriticalItems", con);
            string count = cmd.ExecuteScalar().ToString();
            con.Close();


            int i = 0;
            con.Open();
            cmd = new MySqlCommand("select * from vwCriticalItems", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                critical += i + ". " + dr["pdesc"].ToString() + Environment.NewLine;
            }
            dr.Close();
            con.Close();

            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.close2;
            popup.TitleText = count + " CRITICAL ITEM(S)";
            popup.ContentText = critical;
            popup.Popup();
        }

        private void frmPos_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        public void GetTransNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transno;
                int count;
                con.Open();
                cmd = new MySqlCommand("select transno from tblCart  where transno like '" +sdate +"%' order by id desc", con);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows) { 
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lbltransno.Text = sdate + (count + 1);
                } 
                else { 
                    transno = sdate + "1001";
                    lbltransno.Text = transno;
                } dr.Close();
                con.Close();
            }catch(Exception ex){
                con.Close();
                MessageBox.Show(ex.Message,stitle,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnNewTransaction_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                return;
            }
            GetTransNo();
            txtSearch.Enabled = true;
            txtSearch.Focus();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }

        

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text == string.Empty) { return; }
                else
                {
                    string _pcode;
                    double _price;
                    int _qty;
                    con.Open();
                    cmd = new MySqlCommand("SELECT * FROM tblproduct where barcode like '" + txtSearch.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        qty = int.Parse(dr["qty"].ToString());
                        
                       // frmQty frm = new frmQty(this);
                       // frm.ProductDetails(dr["pcode"].ToString(), double.Parse(dr["price"].ToString()), lbltransno.Text,qty);

                        _pcode = dr["pcode"].ToString();
                        _price = double.Parse(dr["price"].ToString());
                        _qty = int.Parse(txtQty.Text);

                        dr.Close();
                        con.Close();
                        //frm.ShowDialog();
                        AddToCart(_pcode,_price,_qty);
                    }
                    else
                    {
                    dr.Close();
                    con.Close();

                    }

                }


            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void AddToCart( string _pcode, double _price, int _qty)
        {
            string id = "";
            bool found = false;
            int cart_qty=0;
            con.Open();
            cmd = new MySqlCommand("select * from tblcart where transno = @transno and pcode = @pcode", con);
            cmd.Parameters.AddWithValue("@transno",lbltransno.Text);
            cmd.Parameters.AddWithValue("@pcode", _pcode);
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
                    MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                con.Open();
                cmd = new MySqlCommand("update tblcart set qty = (qty + " + _qty + ") where id = '" + id + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
                LoadCart();
                //this.Dispose();
            }
            else
            {

                if (qty < int.Parse(txtQty.Text))
                {
                    MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                con.Open();
                cmd = new MySqlCommand("insert into tblcart(transno,pcode,price,qty,sdate,cashier) VALUES(@transno,@pcode,@price,@qty,@sdate,@cashier)", con);
                cmd.Parameters.AddWithValue("@transno", lbltransno.Text);
                cmd.Parameters.AddWithValue("@pcode", _pcode);
                cmd.Parameters.AddWithValue("@price", _price);
                cmd.Parameters.AddWithValue("@qty", _qty);
                cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                cmd.Parameters.AddWithValue("@cashier",lblUsername.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength=txtSearch.Text.Length;
                LoadCart();
                //this.Dispose();
            }
        }
        public void GetCartTotal()
        {
            
            double discount = Double.Parse(lblDiscount.Text);
            double sales = Double.Parse(lblTotal.Text);
            double vat = sales * dbCon.GetVat();
            double vatable = sales - vat;
            //lblTotal.Text = sales.ToString("#,##0.00");
            lblVat.Text = vat.ToString("#,##0.00");
            lblVatable.Text = vatable.ToString("#,##0.00");
            lblDisplayTotal.Text = sales.ToString("#,##0.00");


        }

        public void LoadCart()
        {
            try
            {
                Boolean hasRecords = false;
                dataGridView1.Rows.Clear();
                int i=0;
                double total = 0;
                double discount = 0;
                con.Open();
                cmd = new MySqlCommand("select c.id, c.pcode,p.pdesc,c.price,c.qty,c.disc,c.total from tblcart as c inner JOIN tblproduct as p on c.pcode = p.pcode where transno like '"+lbltransno.Text+"' and status like 'Pending'", con);
                dr = cmd.ExecuteReader();
              
                while (dr.Read())
                {
                    i++;
                    total += Double.Parse(dr["total"].ToString());
                    discount += Double.Parse(dr["disc"].ToString());
                    dataGridView1.Rows.Add(i, dr["id"].ToString(),dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), Double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
                   
                    hasRecords = true;
                }
               
                dr.Close();
                con.Close();
                lblTotal.Text = total.ToString("#,##0.00");
                lblDiscount.Text = discount.ToString("#,##0.00");
                GetCartTotal();
                if (hasRecords == true) { btnSettle.Enabled = true; btnDiscount.Enabled = true; btnClearCart.Enabled = true; } else { btnSettle.Enabled = false; btnDiscount.Enabled = false; btnClearCart.Enabled = false; }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show(ex.Message,stitle,MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("UNABLE TO LOGOUT. PLEASE CANCEL THE TRANSACTION", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("LOGOUT APPLICATION?", "LOGOUT", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Hide();
                frmSecurity frm = new frmSecurity();
                frm.ShowDialog();
                
            }
            
            
        }

        

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            if (lbltransno.Text == "0000000000000") { return; }
            frmLookup frm = new frmLookup(this);
            frm.LoadRecords();
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ColName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (ColName == "Delete")
            {
                if (MessageBox.Show("DELETE THIS PRODUCT?", "REMOVE ITEM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd= new MySqlCommand("delete from tblcart where id like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("ITEM DELETED SUCCESSFULLY",stitle,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    LoadCart();
                }
            }
            else if (ColName == "colAdd")
            {
                int i = 0;
                con.Open();
                cmd = new MySqlCommand("select sum(qty) as qty from tblproduct where pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "' group by pcode", con);
                i = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                if (int.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) < i)
                {
                    con.Open();
                    cmd = new MySqlCommand("update tblcart set qty=qty + " + int.Parse(txtQty.Text) + " where transno like  '" +lbltransno.Text + "' and pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadCart();
                }
                else
                {
                    MessageBox.Show("REMAINING QTY ON HAND IS " + i + "!","OUT IS STOCK", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (ColName == "colRemove")
            {
                int i = 0;
                con.Open();
                cmd = new MySqlCommand("select sum(qty) as qty from tblcart where pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "' and transno like '" + lbltransno.Text + "' group by transno, pcode", con);
                i = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();
                if (i > 0)
                {
                    con.Open();
                    cmd = new MySqlCommand("update tblcart set qty = qty - " + int.Parse(txtQty.Text) + " where transno like  '" + lbltransno.Text + "' and pcode like '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadCart();
                }
                else
                {
                    MessageBox.Show("REMAINING QTY ON CART  IS " + i + "!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            frmDiscount frm = new frmDiscount(this);
            frm.lblID.Text = id;
            frm.txtPrice.Text = price;
            frm.ShowDialog();


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            id = dataGridView1[1, i].Value.ToString();
            price = dataGridView1[7, i].Value.ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate1.Text = DateTime.Now.ToLongDateString();

        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            frmSettle frm = new frmSettle(this);
            frm.txtSales.Text = lblDisplayTotal.Text;
            frm.ShowDialog();
        }

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            frmSoldItems frm = new frmSoldItems();
            frm.dt1.Enabled = false;
            frm.dt2.Enabled = false;
            frm.suser = lblUsername.Text;
            frm.cboCashier.Enabled = false;
            frm.cboCashier.Text = lblUsername.Text;
            frm.ShowDialog();
        }

        private void frmPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnNewTransaction_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearchProduct_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnDiscount_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnSettle_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnClearCart_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnDailySales_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnChangePass_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
            }
            else if (e.KeyCode == Keys.F10)
            {
                btnClose_Click(sender, e);
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(this);
            frm.ShowDialog();
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("REMOVE ALL ITEMS FROM CART?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                cmd = new MySqlCommand("delete from tblcart where transno like '" + lbltransno.Text + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ALL ITEMS HAS BEEN SUCCESSFULLY REMOVED", "REMOVE  ITEMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCart();

            }
        }

        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
