using System;
using System.Drawing;
using System.Windows.Forms;
using SmartWholesale.Models;
using SmartWholesale.UI.Modules;

namespace SmartWholesale.UI.Dashboards
{
    public class SuperAdminDashboard : Form
    {
        private SuperAdmin _user;
        private Label lblWelcome = null!;
        private Button btnUserManagement = null!, btnViewAllUsers = null!, btnReviewRatings = null!, btnStats = null!, btnLogout = null!;

        public SuperAdminDashboard(SuperAdmin user)
        {
            _user = user;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Super Admin Dashboard - SmartWholesale";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel topPanel = new Panel() { Dock = DockStyle.Top, Height = 80, BackColor = Color.LightGray };
            lblWelcome = new Label() { Text = "Welcome, Super Admin", Font = new Font("Arial", 14, FontStyle.Bold), Location = new Point(20, 25), AutoSize = true };
            btnLogout = new Button() { Text = "Log Out", Location = new Point(780, 25), Size = new Size(80, 30), BackColor = Color.Red, ForeColor = Color.White };
            topPanel.Controls.AddRange(new Control[] { lblWelcome, btnLogout });

            FlowLayoutPanel navPanel = new FlowLayoutPanel() { Dock = DockStyle.Top, Height = 50, BackColor = Color.Navy, Padding = new Padding(10) };
            btnUserManagement = CreateNavButton("User Management");
            btnViewAllUsers = CreateNavButton("View All Users");
            btnReviewRatings = CreateNavButton("Review All Ratings");
            btnStats = CreateNavButton("System Statistics");
            navPanel.Controls.AddRange(new Control[] { btnUserManagement, btnViewAllUsers, btnReviewRatings, btnStats });

            DataGridView dgv = new DataGridView() { Dock = DockStyle.Fill, AutoGenerateColumns = true };
            // For now, it will be empty until we link logic

            btnLogout.Click += (s, e) => { new LoginForm().Show(); this.Close(); };
            btnUserManagement.Click += (s, e) => { new UserManagementForm().Show(); };
            btnViewAllUsers.Click += (s, e) => { new UserManagementForm().Show(); };
            btnReviewRatings.Click += (s, e) => { new ReviewRatingsViewForm().Show(); };
            btnStats.Click += (s, e) => { new SystemStatisticsForm().Show(); };
            
            this.Controls.Add(dgv);
            this.Controls.Add(navPanel);
            this.Controls.Add(topPanel);
        }

        private Button CreateNavButton(string text)
        {
            return new Button() { Text = text, Size = new Size(180, 30), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        }
    }
}
