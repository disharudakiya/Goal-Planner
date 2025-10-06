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
    public partial class ucAddTask : UserControl
    {
        private int? editingTaskId = null;

        public ucAddTask(int AddtaskId) : this()
        {
            editingTaskId = AddtaskId;
            LoadTaskData(AddtaskId);
            save_btn.Text = "Update Task";
        }

        public ucAddTask()
        {
            InitializeComponent();
        }




        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucAddTask_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void add_Task_label_Click(object sender, EventArgs e)
        {

        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            txtTitle.Text = "";
            txtDescription.Text = "";
            dtpDueDate.Value = DateTime.Today;
            cmbCategory.SelectedIndex = -1; // Deselect category

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            {
                string title = txtTitle.Text.Trim();
                string description = txtDescription.Text.Trim();
                DateTime dueDate = dtpDueDate.Value.Date;
                string category = cmbCategory.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(category))
                {
                    MessageBox.Show("Please enter Title and select Category.");
                    return;
                }

                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Goal Planner\";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";


                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    if (editingTaskId == null)
                    {
                        // INSERT
                        string query = "INSERT INTO AddTasks (Title, Description, DueDate, Category) VALUES (@Title, @Description, @DueDate, @Category)";
                        cmd = new SqlCommand(query, conn);
                    }
                    else
                    {
                        // UPDATE
                        string query = "UPDATE AddTasks SET Title = @Title, Description = @Description, DueDate = @DueDate, Category = @Category WHERE TaskID = @TaskID";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@TaskID", editingTaskId);
                    }

                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@DueDate", dueDate);
                    cmd.Parameters.AddWithValue("@Category", category);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show(editingTaskId == null ? "Task added successfully!" : "Task updated successfully!");
                }


            }
        }
        private void LoadTaskData(int AddtaskId)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Goal Planner\";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Description, DueDate, Category FROM AddTasks WHERE AddTaskID = @AddTaskID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AddTaskID", AddtaskId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtTitle.Text = reader["Title"].ToString();
                    txtDescription.Text = reader["Description"].ToString();
                    dtpDueDate.Value = Convert.ToDateTime(reader["DueDate"]);
                    cmbCategory.SelectedItem = reader["Category"].ToString();
                }
                conn.Close();
            }
        }

    }
}