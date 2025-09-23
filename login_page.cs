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

namespace Goal_Planner
{
    public partial class login_page : Form
    {
        public login_page()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void password_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            string enteredPassword = password_textbox.Text.Trim();

            using (SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Goal Planner\";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True"))
            {
                conn.Open();

                string query = "SELECT Password FROM AppPassword WHERE Id = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();

                conn.Close();

                if (result != null)
                {
                    string savedPassword = result.ToString().Trim();

                    if (enteredPassword == savedPassword)
                    {
                        MessageBox.Show("✅ Login successful!");
                        this.Hide();
                        
                    }
                    else
                    {
                        MessageBox.Show("❌ Incorrect password.");
                        password_textbox.Clear();
                        password_textbox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("⚠️ No password found in database.");
                }
            }
        }
    }
}
