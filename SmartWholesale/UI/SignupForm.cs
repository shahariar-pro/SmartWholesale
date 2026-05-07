using System;
using System.Drawing;
using System.Windows.Forms;
using SmartWholesale.BLL;
using SmartWholesale.Models;

namespace SmartWholesale.UI
{
    public class SignupForm : Form
    {
        private TextBox txtName = null!, txtEmail = null!, txtPhone = null!, txtAddress = null!, txtPassword = null!, txtConfirmPass = null!;
        private ComboBox cmbRole = null!;
        private Button btnSignup = null!, btnCancel = null!;
        private AuthService _authService;

        public SignupForm()
        {
            _authService = new AuthService();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "SmartWholesale - Sign Up";
            this.Size = new Size(450, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label() { Text = "Create New Account", Font = new Font("Arial", 16, FontStyle.Bold), Location = new Point(50, 20), Size = new Size(300, 30) };

            int y = 70;
            Label lblName = new Label() { Text = "Full Name:", Location = new Point(50, y), Size = new Size(100, 20) };
            txtName = new TextBox() { Location = new Point(160, y), Size = new Size(200, 20) }; y += 40;

            Label lblEmail = new Label() { Text = "Email:", Location = new Point(50, y), Size = new Size(100, 20) };
            txtEmail = new TextBox() { Location = new Point(160, y), Size = new Size(200, 20) }; y += 40;

            Label lblPhone = new Label() { Text = "Phone Number:", Location = new Point(50, y), Size = new Size(100, 20) };
            txtPhone = new TextBox() { Location = new Point(160, y), Size = new Size(200, 20) }; y += 40;

            Label lblAddr = new Label() { Text = "Address:", Location = new Point(50, y), Size = new Size(100, 20) };
            txtAddress = new TextBox() { Location = new Point(160, y), Size = new Size(200, 20) }; y += 40;

            Label lblRole = new Label() { Text = "Role:", Location = new Point(50, y), Size = new Size(100, 20) };
            cmbRole = new ComboBox() { Location = new Point(160, y), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRole.Items.AddRange(new string[] { "Admin", "Manager", "Customer" }); y += 40;

            Label lblPass = new Label() { Text = "Password:", Location = new Point(50, y), Size = new Size(100, 20) };
            txtPassword = new TextBox() { Location = new Point(160, y), Size = new Size(200, 20), PasswordChar = '*' }; y += 40;

            Label lblConfirm = new Label() { Text = "Confirm Password:", Location = new Point(50, y), Size = new Size(100, 20) };
            txtConfirmPass = new TextBox() { Location = new Point(160, y), Size = new Size(200, 20), PasswordChar = '*' }; y += 60;

            btnSignup = new Button() { Text = "Sign Up", Location = new Point(160, y), Size = new Size(90, 35), BackColor = Color.Blue, ForeColor = Color.White };
            btnCancel = new Button() { Text = "Cancel", Location = new Point(270, y), Size = new Size(90, 35) };

            btnSignup.Click += BtnSignup_Click;
            btnCancel.Click += (s, e) => { new LoginForm().Show(); this.Close(); };

            this.Controls.AddRange(new Control[] { lblTitle, lblName, txtName, lblEmail, txtEmail, lblPhone, txtPhone, lblAddr, txtAddress, lblRole, cmbRole, lblPass, txtPassword, lblConfirm, txtConfirmPass, btnSignup, btnCancel });
        }

        private void BtnSignup_Click(object? sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            UserBase user;
            string role = cmbRole.SelectedItem?.ToString() ?? "Customer";

            switch (role)
            {
                case "Admin": user = new Admin(); break;
                case "Manager": user = new Manager(); break;
                default: user = new Customer(); break;
            }

            user.UName = txtName.Text;
            user.Email = txtEmail.Text;
            user.UPhoneNo = txtPhone.Text;
            user.UAddress = txtAddress.Text;
            user.UPassword = txtPassword.Text;

            if (_authService.Register(user))
            {
                MessageBox.Show("Registration Successful! Please Login.");
                new LoginForm().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration Failed.");
            }
        }
    }
}
