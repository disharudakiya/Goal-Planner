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
            {
                this.Main_panel = new System.Windows.Forms.Panel();
                this.txtPassword = new System.Windows.Forms.TextBox();
                this.lblTitle = new System.Windows.Forms.Label();
                this.btnLogin = new System.Windows.Forms.Button();
                this.linkForgot = new System.Windows.Forms.LinkLabel();
                this.picLock = new System.Windows.Forms.PictureBox();
                this.Main_panel.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.picLock)).BeginInit();
                this.SuspendLayout();

                // panelMain
                this.Main_panel.BackColor = System.Drawing.Color.WhiteSmoke;
                this.Main_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.Main_panel.Controls.Add(this.picLock);
                this.Main_panel.Controls.Add(this.linkForgot);
                this.Main_panel.Controls.Add(this.btnLogin);
                this.Main_panel.Controls.Add(this.lblTitle);
                this.Main_panel.Controls.Add(this.txtPassword);
                this.Main_panel.Location = new System.Drawing.Point(0, 0);
                this.Main_panel.Name = "panelMain";
                this.Main_panel.Size = new System.Drawing.Size(350, 250);

                // picLock
                this.picLock.Image = Properties.Resources.icons8_lock_50__1_; // add an image in Resources
                this.picLock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                this.picLock.Location = new System.Drawing.Point(130, 10);
                this.picLock.Size = new System.Drawing.Size(90, 80);

                // lblTitle
                this.lblTitle.AutoSize = true;
                this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
                this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
                this.lblTitle.Location = new System.Drawing.Point(100, 100);
                this.lblTitle.Text = "Enter Password";

                // txtPassword
                this.txtPassword.Location = new System.Drawing.Point(70, 140);
                this.txtPassword.Size = new System.Drawing.Size(200, 30);
                this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
                this.txtPassword.UseSystemPasswordChar = true;

                // btnLogin
                this.btnLogin.Text = "Login";
                this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
                this.btnLogin.BackColor = System.Drawing.Color.DodgerBlue;
                this.btnLogin.ForeColor = System.Drawing.Color.White;
                this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnLogin.Location = new System.Drawing.Point(70, 180);
                this.btnLogin.Size = new System.Drawing.Size(200, 35);
                this.btnLogin.Click += new System.EventHandler(this.Login_Button_Click);

                // linkForgot
                this.linkForgot.Text = "Forgot Password?";
                this.linkForgot.Location = new System.Drawing.Point(120, 220);
                this.linkForgot.AutoSize = true;
                this.linkForgot.LinkColor = System.Drawing.Color.Blue;

                // LoginForm
                this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.LightBlue;
                this.ClientSize = new System.Drawing.Size(800, 450);
                this.Controls.Add(this.Main_panel);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Load += new System.EventHandler(this.Form1_Load);

                this.Main_panel.ResumeLayout(false);
                this.Main_panel.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.picLock)).EndInit();
                this.ResumeLayout(false);
            } 
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
            
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void Main_panel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
