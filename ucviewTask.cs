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
        private DataTable allTasks = new DataTable(); // store all data for filtering
        public ucviewTask()
        {
            InitializeComponent();
            dgvTasks.MultiSelect = false;
            dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTasks.CellContentClick += dgvTasks_CellContentClick;

            // Setup category ComboBox
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All");
            cmbCategory.Items.Add("Work");
            cmbCategory.Items.Add("Personal");
            cmbCategory.Items.Add("Study");
            cmbCategory.SelectedIndex = 0; // Default to "All"
            this.cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
        }

        public void LoadAllTasks()
        {
            LoadBtn_Click(null, null);
        }

        // Example usage: Trigger the Edit event when a task is selected for editing
        private void dgvTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is on a valid row and not a header or new row
            if (e.RowIndex < 0 || e.RowIndex >= dgvTasks.Rows.Count || dgvTasks.Rows[e.RowIndex].IsNewRow)
                return;

            // Ensure the click is on a valid column
            if (e.ColumnIndex < 0 || e.ColumnIndex >= dgvTasks.Columns.Count)
                return;

            // Check if the cell has a value before accessing it
            var cell = dgvTasks.Rows[e.RowIndex].Cells["AddTaskID"];
            if (cell.Value == null || cell.Value == DBNull.Value)
                return;

            int taskId = Convert.ToInt32(cell.Value);

            // EDIT
            if (dgvTasks.Columns[e.ColumnIndex].Name == "Edit")
            {
                // Hide this view and show edit page with prefilled data
                ucAddTask editPage = new ucAddTask(taskId); // Assumes ucAddTask loads data by ID
                editPage.Dock = DockStyle.Fill;
                var parent = this.Parent;
                this.Hide();
                parent.Controls.Add(editPage);
                editPage.BringToFront();

                // Handle update/cancel to return to view
                editPage.EditCompleted += (s, ev) =>
                {
                    parent.Controls.Remove(editPage);
                    this.Show();
                    LoadBtn_Click(null, null);
                };
                editPage.CancelRequested += (s, ev) =>
                {
                    parent.Controls.Remove(editPage);
                    this.Show();
                };
            }
            // DELETE
            else if (dgvTasks.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Goal Planner\";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";
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
                    this.BeginInvoke(new Action(() => LoadBtn_Click(null, null)));
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

                // Store all data for filtering
                allTasks = dt;

                // Always use ApplyFilters to show all or filtered data
                ApplyFilters();

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

        private void Searchbtn_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();


        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();


        }

        private void ApplyFilters()
        {
            if (allTasks == null || allTasks.Rows.Count == 0)
                return;

            string searchText = txtSearch.Text.Trim().ToLower();
            string selectedCategory = cmbCategory.SelectedItem?.ToString();

            IEnumerable<DataRow> filteredRows = allTasks.AsEnumerable();

            // Filter by search text (Title or Description)
            if (!string.IsNullOrEmpty(searchText))
                filteredRows = filteredRows.Where(r =>
                    (r.Field<string>("Title")?.ToLower().Contains(searchText) == true) ||
                    (r.Field<string>("Description")?.ToLower().Contains(searchText) == true)
                );

            // Filter by category
            if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "All")
                filteredRows = filteredRows.Where(r => r.Field<string>("Category") == selectedCategory);

            // If no filter is applied, show all data
            if (string.IsNullOrEmpty(searchText) && (string.IsNullOrEmpty(selectedCategory) || selectedCategory == "All"))
            {
                dgvTasks.DataSource = allTasks;
            }
            else if (filteredRows.Any())
            {
                // Show filtered and sorted data
                dgvTasks.DataSource = filteredRows.OrderBy(r => r.Field<DateTime>("DueDate")).CopyToDataTable();
            }
            else
            {
                dgvTasks.DataSource = null;
            }
        }
    }
}