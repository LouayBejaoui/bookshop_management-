using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bookShop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Adminlogin obj = new Adminlogin();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\louay\Documents\BookShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        public static string username = "";

        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open(); 

            SqlDataAdapter sda = new SqlDataAdapter("Select count (*) from UserTbl where Uname ='" + UnameTb.Text+ "' and Upassword = '" + UpassTb.Text+"';", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                username = UnameTb.Text;
                Billing obj = new Billing();
                obj.Show();
                this.Hide();
                Con.Close();
            }
            else { 

                MessageBox.Show("Wrong User Info");
    
            }
            Con.Close();    
        }
    }
}
