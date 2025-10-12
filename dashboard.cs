using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Goal_Planner
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
            ShowTaskList();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            ucAddTask addPage = new ucAddTask();
            addPage.Dock = DockStyle.Fill;
            panel3.Controls.Add(addPage);
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            ShowTaskList();
        }

        private void ShowTaskList()
        {
            panel3.Controls.Clear();
            ucTaskList taskListPage = new ucTaskList();
            taskListPage.Dock = DockStyle.Fill;
            panel3.Controls.Add(taskListPage);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void view_btn_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            ucviewTask viewPage = new ucviewTask();
            viewPage.Dock = DockStyle.Fill;
            panel3.Controls.Add(viewPage);

            // Load all tasks immediately
            viewPage.LoadAllTasks();
        }

        private void setting_btn_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            ucSettings addPage = new ucSettings();
            addPage.Dock = DockStyle.Fill;
            panel3.Controls.Add(addPage);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dashboard_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?\nClick OK to return to Login page.",
                                        "Confirm Exit",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                // Hide current Dashboard form
                this.Hide();

                // Show Login form
                login_page login = new login_page();
                login.Show();
            }
        }
    }
}
