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
    public partial class ucSettings : UserControl
    {
        String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Goal Planner\";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";
        public ucSettings()
        {
            InitializeComponent();
        }

        private void ucSettings_Load(object sender, EventArgs e)
        {

        }

        private void panelSettings_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            String enteredPassword = textBox1.Text;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            String query = "update AppPassword set Password=@pass";
            SqlCommand cmd = new SqlCommand(query, conn);   
            cmd.Parameters.AddWithValue("@pass", enteredPassword);
            int result = cmd.ExecuteNonQuery();
            if (result == -1) {
                MessageBox.Show("Error updating password");
            }
            else
            {
                MessageBox.Show("Password updated successfully");
                textBox1.Clear();
                textBox1.Focus();
            }

            conn.Close();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            String selected = comboBox1.Text;
            MessageBox.Show($"{selected}");
        }
    }
}
