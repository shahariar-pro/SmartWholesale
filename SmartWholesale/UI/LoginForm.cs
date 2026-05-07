using System;
using System.Drawing;
using System.Windows.Forms;
using SmartWholesale.BLL;
using SmartWholesale.Models;

namespace SmartWholesale.UI
{
    public class LoginForm : Form
    {
        private TextBox txtEmail = null!;
        private TextBox txtPassword = null!;
        private Button btnLogin = null!;
        private Button btnClear = null!;
        private LinkLabel lnkSignup = null!;
        private AuthService _authService;

        public LoginForm()
        {
            _authService = new AuthService();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "SmartWholesale - Login";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblTitle = new Label()
            {
                Text = "SmartWholesale",
                Font = new Font("Arial", 20, FontStyle.Bold),
                Location = new Point(50, 30),
                Size = new Size(300, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblEmail = new Label() { Text = "Email (ID):", Location = new Point(50, 100), Size = new Size(100, 20) };
            txtEmail = new TextBox() { Location = new Point(150, 100), Size = new Size(180, 20) };

            Label lblPassword = new Label() { Text = "Password:", Location = new Point(50, 140), Size = new Size(100, 20) };
            txtPassword = new TextBox() { Location = new Point(150, 140), Size = new Size(180, 20), PasswordChar = '*' };

            btnLogin = new Button() { Text = "Login", Location = new Point(150, 180), Size = new Size(80, 30), BackColor = Color.Blue, ForeColor = Color.White };
            btnClear = new Button() { Text = "Clear", Location = new Point(250, 180), Size = new Size(80, 30) };

            Label lblNoAccount = new Label() { Text = "Don't have an account?", Location = new Point(120, 230), Size = new Size(150, 20) };
            lnkSignup = new LinkLabel() { Text = "Sign Up Here", Location = new Point(150, 250), Size = new Size(100, 20) };

            btnLogin.Click += BtnLogin_Click;
            btnClear.Click += (s, e) => { txtEmail.Clear(); txtPassword.Clear(); };
            lnkSignup.LinkClicked += (s, e) => { new SignupForm().Show(); this.Hide(); };

            this.Controls.AddRange(new Control[] { lblTitle, lblEmail, txtEmail, lblPassword, txtPassword, btnLogin, btnClear, lblNoAccount, lnkSignup });
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            var user = _authService.Authenticate(txtEmail.Text, txtPassword.Text);
            if (user != null)
            {
                MessageBox.Show(user.GetDashboardInfo(), "Login Success");
                Form? dashboard = null;

                switch (user.Role)
                {
                    case "SuperAdmin": dashboard = new Dashboards.SuperAdminDashboard((SuperAdmin)user); break;
                    case "Admin": dashboard = new Dashboards.AdminDashboard((Admin)user); break;
                    case "Manager": dashboard = new Dashboards.ManagerDashboard((Manager)user); break;
                    case "Customer": dashboard = new Dashboards.CustomerDashboard((Customer)user); break;
                }

                if (dashboard != null)
                {
                    dashboard.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Invalid Email or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
