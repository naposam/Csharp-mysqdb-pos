using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace POSNew
{
    public class DbConnection
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;
        private double dailysales;
        private int productLine;
        private int stockonHand;
        private int criticalItem;
        private string con;
        public string getConnection()
        {
            con=@"datasource=localhost;port=3306;username=root;password=;database=csharp";
             return con;
        }

        public double DailySales()
        {

            string sdate = DateTime.Now.ToString("yyyy-MM-dd");
            cn = new MySqlConnection();
            cn.ConnectionString = con;
            cn.Open();
            cmd = new MySqlCommand("SELECT IFNULL(sum(total),0) as total FROM tblcart WHERE sdate  BETWEEN '" + sdate + "'  and '" + sdate + "'   and status like 'sold'", cn);
            dailysales = double.Parse(cmd.ExecuteScalar().ToString());
            cn.Close();
            return dailysales;
        }

        public double ProductLine()
        {
            cn = new MySqlConnection();
            cn.ConnectionString = con;
            cn.Open();
            cmd = new MySqlCommand("SELECT COUNT(*) FROM tblproduct ", cn);
            productLine = int.Parse(cmd.ExecuteScalar().ToString());
            cn.Close();
            return productLine;
        }

        public double StockOnHand()
        {
            cn = new MySqlConnection();
            cn.ConnectionString = con;
            cn.Open();
            cmd = new MySqlCommand("SELECT sum(qty) as qty FROM tblproduct ", cn);
            stockonHand = int.Parse(cmd.ExecuteScalar().ToString());
            cn.Close();
            return stockonHand;
        }
        public double CrititalItems()
        {
            cn = new MySqlConnection();
            cn.ConnectionString = con;
            cn.Open();
            cmd = new MySqlCommand("SELECT COUNT(*) FROM vwcriticalitems", cn);
            criticalItem = int.Parse(cmd.ExecuteScalar().ToString());
            cn.Close();
            return criticalItem;
        }

        public double GetVat()
        {
            double vat=0;
            cn.ConnectionString = getConnection();
            cn.Open();
            cmd = new MySqlCommand("select * from tblvat", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                vat = Double.Parse(dr["vat"].ToString());
            }
            dr.Close();
            cn.Close();
            return vat;

        }
        public string GetPassword(string user)
        {
            string password="";
            cn.ConnectionString = getConnection();
            cn.Open();
            cmd = new MySqlCommand("select * from tbluser where username =@username", cn);
            cmd.Parameters.AddWithValue("@username", user);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
             password= dr["password"].ToString();
            }
           
            dr.Close();
            cn.Close();
            return password;
        }
        }
    }

