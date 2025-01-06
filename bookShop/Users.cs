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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            Populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\louay\Documents\BookShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void Populate()
        {
            Con.Open();
            string query = "Select * from Usertbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserData.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            Uname.Text = "";
            Uphone.Text = "";
            Uaddress.Text = "";
            Upassword.Text = "";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Uname.Text == "" || Uphone.Text == "" ||  Uaddress.Text == "" || Upassword.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into Usertbl Values('" + Uname.Text + "','" + Uphone.Text + "','" + Uaddress.Text + "','" + Upassword.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User saved successfully");
                    Con.Close();
                    Populate();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        int key = 0;

        private void UserData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Uname.Text = UserData.SelectedRows[0].Cells[1].Value.ToString();
            Uphone.Text = UserData.SelectedRows[0].Cells[2].Value.ToString();
            Uaddress.Text = UserData.SelectedRows[0].Cells[3].Value.ToString();
            Upassword.Text = UserData.SelectedRows[0].Cells[4].Value.ToString();

            if (Uname.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(key = Convert.ToInt32(UserData.SelectedRows[0].Cells[0].Value.ToString()));
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Delete from UserTbl where Uid = " + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User deleted successfully");
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

        private void button4_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Uname.Text == "" || Uphone.Text == ""  || Uaddress.Text == "" || Upassword.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update UserTbl set Uname = '" + Uname.Text + "', Uphone ='" + Uphone.Text + "', Uaddress ='" + Uaddress.Text + "', Upassword ='" + Upassword.Text + "' where Uid = " + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User updated successfully");
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

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       

        private void label5_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void Users_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Books obj = new Books();
            obj.Show();
            this.Hide();
        }
    }
}
