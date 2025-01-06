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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

       
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Books obj = new Books();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\louay\Documents\BookShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Dashboard_Load(object sender, EventArgs e)
        {
            Con.Open();
            //books total
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(Bqtt) from Booktbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DTotalBooks.Text = dt.Rows[0][0].ToString();

            //Amount total
            SqlDataAdapter sda1 = new SqlDataAdapter("Select Sum(Amount) from Billtbl", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            DTotalAmount.Text = dt1.Rows[0][0].ToString();

            //Users total

            SqlDataAdapter sda2 = new SqlDataAdapter("Select count (*) from UserTbl", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            DTotalUsers.Text = dt2.Rows[0][0].ToString();
            Con.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
