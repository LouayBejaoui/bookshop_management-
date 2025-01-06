using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
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
        int n = 0, gridTotal=0;

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (BquantityTb.Text=="" ||Convert.ToInt32(BquantityTb.Text) > stock)
            {
                MessageBox.Show("Not enough in stock!");
            }else
            {
                int total = Convert.ToInt32(BquantityTb.Text) * Convert.ToInt32(PriceTb.Text);
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                dataGridViewRow.CreateCells(Billdata);
                dataGridViewRow.Cells[0].Value = n+1;
                dataGridViewRow.Cells[1].Value = BtitleTb.Text;
                dataGridViewRow.Cells[2].Value = PriceTb.Text;
                dataGridViewRow.Cells[3].Value = BquantityTb.Text;
                dataGridViewRow.Cells[4].Value = total;
                Billdata.Rows.Add(dataGridViewRow);
                n++;
                UpdateBooks();
                gridTotal = gridTotal + total;
                totalLabel.Text = gridTotal.ToString() + "$";
                
            }
        }

    

        int key = 0 , stock = 0;
        private void BooksData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BtitleTb.Text = BooksData.SelectedRows[0].Cells[1].Value.ToString();
            PriceTb.Text = BooksData.SelectedRows[0].Cells[5].Value.ToString();
            

            if (BtitleTb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(BooksData.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(BooksData.SelectedRows[0].Cells[4].Value.ToString());
            }
        }
        private void UpdateBooks()
        {
            
            int newQ = stock - Convert.ToInt32(BquantityTb.Text);

            try
            {
                Con.Open();
                string query = "update Booktbl set Bqtt =" + newQ + " where Bid = " + key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                Populate();
                key = 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

       
        private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();

       

        private void EditBtn_Click(object sender, EventArgs e)
        {
            
            if (BquantityTb.Text == "" || ClientnameTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into Billtbl Values('" + username.Text + "','" + ClientnameTb.Text + "','" + gridTotal + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill saved successfully");
                    Con.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("CustomSize", 827, 1169); // Width and Height in hundredths of an inch

                // Show the print preview dialog
                printPreviewDialog1.Document = printDocument1; // Attach the document to preview
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    // If the user clicks OK, send the document to the printer
                    printDocument1.Print();
                }
                
            }
        }
        int prodid, prodprice, prodqty, total, pos=60;

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

        private void Billing_Load(object sender, EventArgs e)
        {
            username.Text = Login.username;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Book Shop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("Id product price quantity total", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach (DataGridViewRow row in Billdata.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                total = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + total, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos += 20;
            }
            e.Graphics.DrawString("Grand Total : " + gridTotal + "$", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(26, pos + 50));
            e.Graphics.DrawString("***********Book Store***********", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(40, pos + 85));
            Billdata.Rows.Clear();
            Billdata.Refresh();
            pos = 100;
            gridTotal = 0;
        }

        private void Reset()
        {
            BtitleTb.Text = "";
            BquantityTb.Text = "";
            PriceTb.Text = "";
            ClientnameTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
