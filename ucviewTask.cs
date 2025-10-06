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
    public partial class ucviewTask : UserControl
    {
        public ucviewTask()
        {
            InitializeComponent();
        }
    


        // Example usage: Trigger the Edit event when a task is selected for editing
        private void dgvTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int taskId = Convert.ToInt32(dgvTasks.Rows[e.RowIndex].Cells["AddTaskID"].Value);

                // EDIT
                if (dgvTasks.Columns[e.ColumnIndex].Name == "Edit")
                {
                    // open Add Task page in Edit Mode
                    ucAddTask editPage = new ucAddTask(taskId);
                    editPage.Dock = DockStyle.Fill;
                    this.Parent.Controls.Clear();
                    this.Parent.Controls.Add(editPage);
                }

                // DELETE
                else if (dgvTasks.Columns[e.ColumnIndex].Name == "Delete")
                {
                    if (MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Goal Planner\";Integrated Security=True;TrustServerCertificate=True;";
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            string query = "DELETE FROM AddTasks WHERE AddTaskID=@TaskID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@TaskID", taskId);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        MessageBox.Show("Task deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBtn_Click(null, null); // reload grid
                    }
                }
            }
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Goal Planner\";Integrated Security=True;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT AddTaskID, Title, DueDate, Category, Description FROM AddTasks";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvTasks.DataSource = dt;

                // Style
                dgvTasks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                dgvTasks.EnableHeadersVisualStyles = false;
                dgvTasks.RowTemplate.Height = 60;
                dgvTasks.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                dgvTasks.DefaultCellStyle.Padding = new Padding(5, 2, 5, 2);
                dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Add Edit/Delete icons (once only)
                if (!dgvTasks.Columns.Contains("Edit"))
                {
                    DataGridViewImageColumn editCol = new DataGridViewImageColumn();
                    editCol.Name = "Edit";
                    editCol.HeaderText = "Edit";
                    editCol.Image = Properties.Resources.edit_table; // your edit icon
                    editCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dgvTasks.Columns.Add(editCol);
                }

                if (!dgvTasks.Columns.Contains("Delete"))
                {
                    DataGridViewImageColumn deleteCol = new DataGridViewImageColumn();
                    deleteCol.Name = "Delete";
                    deleteCol.HeaderText = "Delete";
                    deleteCol.Image = Properties.Resources.bin; // your delete icon
                    deleteCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dgvTasks.Columns.Add(deleteCol);
                }
            }
        }
    }
    }
