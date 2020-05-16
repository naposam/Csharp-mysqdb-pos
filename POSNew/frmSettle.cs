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
    public partial class frmSettle : Form
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        DbConnection dbCon = new DbConnection();
        MySqlDataReader dr;
        frmPos fpos;
        public frmSettle( frmPos fp)
        {
            InitializeComponent();
            con = new MySqlConnection(dbCon.getConnection());
            fpos = fp;
            this.KeyPreview = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double sale = Double.Parse(txtSales.Text);
               double cash = Double.Parse(txtCash.Text);
               double change = cash - sale;
               txtChange.Text = change.ToString("#,##0.00");

            }
            catch (Exception ex)
            {
                txtChange.Text = "0.00";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn4.Text;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn5.Text;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn6.Text;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn0.Text;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn9.Text;
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn3.Text;
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn00.Text;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Double.Parse(txtChange.Text) < 0) || (txtCash.Text == String.Empty))
                {
                    MessageBox.Show("INSUFFICIENT AMOUNT. PLEASE ENTER THE CORRECT AMOUNT", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    
                    for (int i = 0; i < fpos.dataGridView1.Rows.Count; i++)
                    {
                        con.Open();
                        cmd = new MySqlCommand("update tblproduct set qty= qty - " + int.Parse(fpos.dataGridView1.Rows[i].Cells[5].Value.ToString()) + " where pcode = '" + fpos.dataGridView1.Rows[i].Cells[2].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                         con.Open();
                         cmd = new MySqlCommand("update tblcart set status='sold' where id = '" + fpos.dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", con);
                         cmd.ExecuteNonQuery();
                         con.Close();
                         
                    }
                    frmReceipt frm = new frmReceipt(fpos);
                    frm.LoadReport(txtCash.Text,txtChange.Text);
                     frm.ShowDialog();

                    MessageBox.Show("PAYMENT SUCCESSFULLY SAVED.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fpos.GetTransNo();
                    fpos.LoadCart();
                    this.Dispose();

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("INSUFFICIENT AMOUNT. PLEASE ENTER THE CORRECT AMOUNT", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmSettle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }else if(e.KeyCode== Keys.Enter){
                btnEnter_Click(sender, e);
            }
        }
    }
}
