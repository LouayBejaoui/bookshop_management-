using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace bookShop
{
    public partial class Books : Form
    {
        public Books()
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
        private void Filter()
        {
            Con.Open();
            string query = "Select * from Booktbl where Bcatg = '"+ CatCbSearchCb.SelectedItem + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BooksData.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Populate();
            CatCbSearchCb.SelectedIndex = -1;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(BtitleTb.Text==""||BauthTb.Text =="" ||BcatCb.SelectedIndex==-1 || QtyTb.Text == ""|| PriceTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into Booktbl Values('" + BtitleTb.Text + "','" + BauthTb.Text + "','" + BcatCb.SelectedItem.ToString() + "','" + QtyTb.Text + "','" + PriceTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("books saved successfully");
                    Con.Close();
                    Populate();
                    Reset();

                } catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void CatCbSearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void Reset()
        {
            BtitleTb.Text = "";
            BauthTb.Text = "";
            BcatCb.SelectedIndex = -1;
            QtyTb.Text = "";
            PriceTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        int key = 0;
        private void BooksData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BtitleTb.Text = BooksData.SelectedRows[0].Cells[1].Value.ToString();
            BauthTb.Text = BooksData.SelectedRows[0].Cells[2].Value.ToString();
            BcatCb.SelectedItem = BooksData.SelectedRows[0].Cells[3].Value.ToString();
            QtyTb.Text = BooksData.SelectedRows[0].Cells[4].Value.ToString();
            PriceTb.Text = BooksData.SelectedRows[0].Cells[5].Value.ToString();

            if (BtitleTb.Text == "")
            {
                key = 0;
            }else
            {
                key = Convert.ToInt32(key = Convert.ToInt32(BooksData.SelectedRows[0].Cells[0].Value.ToString()));
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key ==0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Delete from Booktbl where Bid = "+key+ ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("book deleted successfully");
                    Con.Close();
                    Populate();
                    Reset();
                    key = 0;    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (BtitleTb.Text == "" || BauthTb.Text == "" || BcatCb.SelectedIndex == -1 || QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update Booktbl set Btitle = '" + BtitleTb.Text + "', Bauthor ='" + BauthTb.Text + "', Bcatg ='" + BcatCb.SelectedItem.ToString() + "', Bqtt ='" + QtyTb.Text + "', Bprice ='" + PriceTb.Text + "' where Bid = " + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("book updated successfully");
                    Con.Close();
                    Populate();
                    Reset();
                    key = 0;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
