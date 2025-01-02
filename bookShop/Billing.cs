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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            Populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\louay\Documents\BookShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Populate()
        {
            Con.Open();
            string query = "Select * from Booktbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BooksData.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {

        }
        int key = 0;
        private void BooksData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BtitleTb.Text = BooksData.SelectedRows[0].Cells[1].Value.ToString();
            BquantityTb.Text = BooksData.SelectedRows[0].Cells[4].Value.ToString();
            //BcatCb.SelectedItem = BooksData.SelectedRows[0].Cells[3].Value.ToString();
            //QtyTb.Text = BooksData.SelectedRows[0].Cells[4].Value.ToString();
            PriceTb.Text = BooksData.SelectedRows[0].Cells[5].Value.ToString();
            ClientnameTb.Text = 

            if (BtitleTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(key = Convert.ToInt32(BooksData.SelectedRows[0].Cells[0].Value.ToString()));
            }
        }
    }
}
