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
        private AppTheme currentTheme = AppTheme.Light;

        public ucSettings()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Light");
            comboBox1.Items.Add("Dark");
            comboBox1.SelectedIndex = 0;
        }

        private void ucSettings_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = currentTheme == AppTheme.Dark ? "Dark" : "Light";
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
            string selected = comboBox1.Text;
            AppTheme theme = selected == "Dark" ? AppTheme.Dark : AppTheme.Light;
            ApplyTheme(theme);
            MessageBox.Show($"Theme changed to {selected}.");
        }

        public void ApplyTheme(AppTheme theme)
        {
            currentTheme = theme;

            Color backColor, foreColor, panelColor;
            if (theme == AppTheme.Dark)
            {
                backColor = Color.FromArgb(30, 30, 30);
                foreColor = Color.White;
                panelColor = Color.FromArgb(45, 45, 48);
            }
            else
            {
                backColor = Color.White;
                foreColor = Color.Black;
                panelColor = Color.Gainsboro;
            }

            this.BackColor = backColor;

            // Apply recursively to all controls
            ApplyThemeToControls(this.Controls, backColor, foreColor, panelColor);
        }

        private void ApplyThemeToControls(Control.ControlCollection controls, Color backColor, Color foreColor, Color panelColor)
        {
            foreach (Control ctrl in controls)
            {
                // Set back color depending on control type
                if (ctrl is Panel)
                    ctrl.BackColor = panelColor;
                else if (ctrl is Button)
                    ctrl.BackColor = Color.FromArgb(64, 64, 64); // optional darker button color for dark mode
                else
                    ctrl.BackColor = backColor;

                // Set text (fore) color
                ctrl.ForeColor = foreColor;

                // Recursively apply to nested controls
                if (ctrl.HasChildren)
                    ApplyThemeToControls(ctrl.Controls, backColor, foreColor, panelColor);
            }
        }
        private void btnLightTheme_Click(object sender, EventArgs e)
        {
            ApplyTheme(AppTheme.Light);
        }

        private void btnDarkTheme_Click(object sender, EventArgs e)
        {
            ApplyTheme(AppTheme.Dark);
        }
    }
}
