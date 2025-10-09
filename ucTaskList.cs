using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Goal_Planner
{
    public partial class ucTaskList : UserControl
    {
        public ucTaskList()
        {
            InitializeComponent();
            LoadTasks();
        }

        private void LoadTasks()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Goal Planner\";Integrated Security=True;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT AddTaskID, Title, DueDate, Category, Description FROM AddTasks";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                flowPanel.Controls.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    Panel card = new Panel
                    {
                        Height = 110,
                        Width = flowPanel.Width - 60,
                        BackColor = Color.White,
                        Margin = new Padding(15, 10, 15, 10),
                        Padding = new Padding(0),
                    };

                    card.Paint += (s, e) =>
                    {
                        var g = e.Graphics;
                        var rect = card.ClientRectangle;
                        rect.Inflate(-1, -1);
                        using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                        {
                            int radius = 16;
                            path.AddArc(rect.Left, rect.Top, radius, radius, 180, 90);
                            path.AddArc(rect.Right - radius, rect.Top, radius, radius, 270, 90);
                            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                            path.AddArc(rect.Left, rect.Bottom - radius, radius, radius, 90, 90);
                            path.CloseFigure();
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            using (var shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
                                g.FillPath(shadowBrush, path);
                            using (var borderPen = new Pen(Color.LightGray, 2))
                                g.DrawPath(borderPen, path);
                        }
                    };

                    card.MouseEnter += (s, e) => { card.BackColor = Color.FromArgb(245, 250, 255); };
                    card.MouseLeave += (s, e) => { card.BackColor = Color.White; };

                    RadioButton rbTodo = new RadioButton
                    {
                        Text = "",
                        Tag = row["AddTaskID"],
                        Location = new Point(18, 40),
                        AutoSize = true
                    };

                    Label lblTitle = new Label
                    {
                        Text = row["Title"].ToString(),
                        Font = new Font("Segoe UI", 15, FontStyle.Bold),
                        ForeColor = Color.FromArgb(60, 60, 120),
                        Location = new Point(50, 18),
                        AutoSize = true
                    };

                    // Category with icon
                    PictureBox catIcon = new PictureBox
                    {
                        Image = Properties.Resources.shapes, // Add a small icon to Resources
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(20, 20),
                        Location = new Point(50, 55)
                    };

                    Label lblCategory = new Label
                    {
                        Text = row["Category"].ToString(),
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = GetCategoryColor(row["Category"].ToString()),
                        Location = new Point(75, 55),
                        AutoSize = true
                    };

                    // Due date with icon
                    PictureBox dueIcon = new PictureBox
                    {
                        Image = Properties.Resources.calendar, // Add a small icon to Resources
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(20, 20),
                        Location = new Point(200, 55)
                    };

                    Label lblDue = new Label
                    {
                        Text = Convert.ToDateTime(row["DueDate"]).ToString("dd MMM yyyy"),
                        Font = new Font("Segoe UI", 10, FontStyle.Regular),
                        ForeColor = Color.DarkRed,
                        Location = new Point(225, 55),
                        AutoSize = true
                    };

                    // Description
                    Label lblDesc = new Label
                    {
                        Text = row["Description"].ToString(),
                        Font = new Font("Segoe UI", 9, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Location = new Point(50, 80),
                        AutoSize = true
                    };

                    Button btnDone = new Button
                    {
                        Text = "Mark as Done",
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        BackColor = Color.FromArgb(220, 255, 220),
                        ForeColor = Color.SeaGreen,
                        FlatStyle = FlatStyle.Flat,
                        Location = new Point(card.Width - 130, 35),
                        Size = new Size(110, 50),
                        Cursor = Cursors.Hand
                    };
                    btnDone.FlatAppearance.BorderSize = 0;
                    btnDone.Click += (s, e) =>
                    {
                        rbTodo.Checked = true;
                        btnDone.Enabled = false;
                        btnDone.Text = "Completed";
                        btnDone.BackColor = Color.LightGray;
                        btnDone.ForeColor = Color.Gray;
                    };

                    card.Controls.Add(rbTodo);
                    card.Controls.Add(lblTitle);
                    card.Controls.Add(catIcon);
                    card.Controls.Add(lblCategory);
                    card.Controls.Add(dueIcon);
                    card.Controls.Add(lblDue);
                    card.Controls.Add(lblDesc);
                    card.Controls.Add(btnDone);

                    flowPanel.Controls.Add(card);
                }
            }
        }

        private Color GetCategoryColor(string category)
        {
            switch (category.ToLower())
            {
                case "work": return Color.FromArgb(80, 170, 255);
                case "study": return Color.FromArgb(120, 220, 120);
                case "personal": return Color.FromArgb(255, 180, 80);
                default: return Color.FromArgb(180, 180, 180);
            }
        }

        private FlowLayoutPanel flowPanel;
        private Label headerLabel;

        private void InitializeComponent()
        {
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.headerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowPanel
            // 
            this.flowPanel.AutoScroll = true;
            this.flowPanel.BackColor = System.Drawing.Color.White;
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanel.Location = new System.Drawing.Point(0, 60);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Padding = new System.Windows.Forms.Padding(10);
            this.flowPanel.Size = new System.Drawing.Size(600, 340);
            this.flowPanel.TabIndex = 0;
            this.flowPanel.WrapContents = false;
            this.flowPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.flowPanel_Paint);
            // 
            // headerLabel
            // 
            this.headerLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(220)))), ((int)(((byte)(235)))));
            this.headerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(120)))));
            this.headerLabel.Location = new System.Drawing.Point(0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.headerLabel.Size = new System.Drawing.Size(600, 60);
            this.headerLabel.TabIndex = 1;
            this.headerLabel.Text = "Your To-Do Tasks";
            this.headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucTaskList
            // 
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.flowPanel);
            this.Controls.Add(this.headerLabel);
            this.Name = "ucTaskList";
            this.Size = new System.Drawing.Size(600, 400);
            this.ResumeLayout(false);

        }

        private void flowPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}