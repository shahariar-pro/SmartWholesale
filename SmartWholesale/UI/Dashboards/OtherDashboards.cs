using System;
using System.Drawing;
using System.Windows.Forms;
using SmartWholesale.Models;
using SmartWholesale.UI.Modules;

namespace SmartWholesale.UI.Dashboards
{
    public class AdminDashboard : Form
    {
        private Admin _user;
        public AdminDashboard(Admin user) { _user = user; InitializeComponent(); }
        private void InitializeComponent()
        {
            this.Text = "Shop Owner Dashboard - SmartWholesale";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel topPanel = new Panel() { Dock = DockStyle.Top, Height = 80, BackColor = Color.LightGray };
            Label lblWelcome = new Label() { Text = $"Welcome, Shop Owner [{_user.UName}]", Font = new Font("Arial", 14, FontStyle.Bold), Location = new Point(20, 25), AutoSize = true };
            Button btnLogout = new Button() { Text = "Log Out", Location = new Point(780, 25), Size = new Size(80, 30), BackColor = Color.Red, ForeColor = Color.White };
            
            Button btnBulkInfo = new Button() { Text = "!", Location = new Point(740, 25), Size = new Size(30, 30), BackColor = Color.Gold, Font = new Font("Arial", 12, FontStyle.Bold) };
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnBulkInfo, "Click to see Bulk Discount Rules");
            btnBulkInfo.Click += (s, e) => {
                string rules = "Bulk Discount Rules:\n" +
                               "- 10+ items: 5% Off\n" +
                               "- 20+ items: 10% Off\n" +
                               "- 50+ items: 15% Off\n\n" +
                               "Discounts are applied automatically during purchase.";
                MessageBox.Show(rules, "Wholesale Bulk Discounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            btnLogout.Click += (s, e) => { new LoginForm().Show(); this.Close(); };
            topPanel.Controls.AddRange(new Control[] { lblWelcome, btnBulkInfo, btnLogout });

            FlowLayoutPanel navPanel = new FlowLayoutPanel() { Dock = DockStyle.Top, Height = 50, BackColor = Color.Blue, Padding = new Padding(10) };
            string[] navItems = { "My Products", "My Earnings", "Sales Report", "Create Offers", "View Reviews" };
            foreach (var item in navItems)
            {
                Button btn = new Button() { Text = item, Size = new Size(160, 30), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btn.Click += (s, e) => {
                    switch (item)
                    {
                        case "My Products": new ProductManagementForm(_user).Show(); break;
                        case "My Earnings":
                        case "Sales Report": new SalesDashboardForm(_user).Show(); break;
                        case "Create Offers": new SpecialOffersForm().Show(); break;
                        case "View Reviews": new ReviewRatingsViewForm().Show(); break;
                    }
                };
                navPanel.Controls.Add(btn);
            }

            this.Controls.Add(navPanel);
            this.Controls.Add(topPanel);
        }
    }

    public class ManagerDashboard : Form
    {
        private Manager _user;
        public ManagerDashboard(Manager user) { _user = user; InitializeComponent(); }
        private void InitializeComponent()
        {
            this.Text = "Manager Dashboard - SmartWholesale";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel topPanel = new Panel() { Dock = DockStyle.Top, Height = 80, BackColor = Color.LightBlue };
            Label lblWelcome = new Label() { Text = "Manager Dashboard - Inventory Control", Font = new Font("Arial", 14, FontStyle.Bold), Location = new Point(20, 25), AutoSize = true };
            Button btnLogout = new Button() { Text = "Log Out", Location = new Point(780, 25), Size = new Size(80, 30), BackColor = Color.Red, ForeColor = Color.White };
            btnLogout.Click += (s, e) => { new LoginForm().Show(); this.Close(); };
            topPanel.Controls.AddRange(new Control[] { lblWelcome, btnLogout });

            FlowLayoutPanel navPanel = new FlowLayoutPanel() { Dock = DockStyle.Top, Height = 50, BackColor = Color.RoyalBlue, Padding = new Padding(10) };
            string[] navItems = { "Products CRUD", "Low Stock Alerts", "Transactions", "View Reviews" };
            foreach (var item in navItems)
            {
                Button btn = new Button() { Text = item, Size = new Size(180, 30), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btn.Click += (s, e) => {
                    switch (item)
                    {
                        case "Products CRUD": new ProductManagementForm(_user).Show(); break;
                        case "Low Stock Alerts": new LowStockAlertsForm().Show(); break;
                        case "Transactions": new TransactionDashboardForm().Show(); break;
                        case "View Reviews": new ReviewRatingsViewForm().Show(); break;
                    }
                };
                navPanel.Controls.Add(btn);
            }

            this.Controls.Add(navPanel);
            this.Controls.Add(topPanel);
        }
    }

    public class CustomerDashboard : Form
    {
        private Customer _user;
        public CustomerDashboard(Customer user) { _user = user; InitializeComponent(); }
        private void InitializeComponent()
        {
            this.Text = "Customer Dashboard - SmartWholesale";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel topPanel = new Panel() { Dock = DockStyle.Top, Height = 80, BackColor = Color.WhiteSmoke };
            Label lblWelcome = new Label() { Text = $"Welcome, {_user.UName}", Font = new Font("Arial", 14, FontStyle.Bold), Location = new Point(20, 25), AutoSize = true };
            Button btnCart = new Button() { Text = "Cart", Location = new Point(680, 25), Size = new Size(80, 30), BackColor = Color.Blue, ForeColor = Color.White };
            btnCart.Click += (s, e) => { new ShoppingCartForm(_user).Show(); };
            Button btnLogout = new Button() { Text = "Log Out", Location = new Point(780, 25), Size = new Size(80, 30), BackColor = Color.Red, ForeColor = Color.White };
            btnLogout.Click += (s, e) => { new LoginForm().Show(); this.Close(); };
            topPanel.Controls.AddRange(new Control[] { lblWelcome, btnCart, btnLogout });

            FlowLayoutPanel navPanel = new FlowLayoutPanel() { Dock = DockStyle.Top, Height = 50, BackColor = Color.DodgerBlue, Padding = new Padding(10) };
            string[] navItems = { "Browse Products", "My Cart", "My Orders", "My Reviews" };
            foreach (var item in navItems)
            {
                Button btn = new Button() { Text = item, Size = new Size(180, 30), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btn.Click += (s, e) => {
                    switch (item)
                    {
                        case "Browse Products": new ProductManagementForm(_user).Show(); break;
                        case "My Cart": new ShoppingCartForm(_user).Show(); break;
                        case "My Orders": new OrderHistoryForm(_user).Show(); break;
                        case "My Reviews": new ReviewModuleForm(_user).Show(); break;
                    }
                };
                navPanel.Controls.Add(btn);
            }

            this.Controls.Add(navPanel);
            this.Controls.Add(topPanel);
        }
    }
}
