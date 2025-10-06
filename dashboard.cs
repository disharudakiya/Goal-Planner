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

            ucDashboard dashPage = new ucDashboard();
            dashPage.Dock = DockStyle.Fill;
            panel3.Controls.Add(dashPage);
        
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
            panel3.Controls.Clear();
            ucDashboard dashPage = new ucDashboard();
            dashPage.Dock = DockStyle.Fill;
            panel3.Controls.Add(dashPage);



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
            ucviewTask addPage = new ucviewTask();
            addPage.Dock = DockStyle.Fill;
            panel3.Controls.Add(addPage);


            



    
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
    }
}
