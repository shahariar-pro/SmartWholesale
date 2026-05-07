using System;
using System.Drawing;
using System.Windows.Forms;
using SmartWholesale.BLL;
using SmartWholesale.Models;
using SmartWholesale.DAL;
using System.Collections.Generic;

namespace SmartWholesale.UI.Modules
{
    public class UserManagementForm : Form
    {
        private DataGridView dgvUsers = null!;
        private TextBox txtName = null!, txtEmail = null!, txtPhone = null!, txtAddress = null!, txtSalary = null!;
        private ComboBox cmbRole = null!;
        private Button btnAdd = null!, btnUpdate = null!, btnDelete = null!, btnClear = null!;
        private UserRepository _userRepo;

        public UserManagementForm()
        {
            _userRepo = new DAL.UserRepository();
            InitializeComponent();
            LoadUsers();
        }

        private void InitializeComponent()
        {
            this.Text = "User Management - SmartWholesale";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvUsers = new DataGridView() { Location = new Point(20, 20), Size = new Size(600, 500), AutoGenerateColumns = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;

            Panel inputPanel = new Panel() { Location = new Point(640, 20), Size = new Size(320, 500), BorderStyle = BorderStyle.FixedSingle };
            
            int y = 20;
            Label lblName = new Label() { Text = "Name:", Location = new Point(10, y) };
            txtName = new TextBox() { Location = new Point(110, y), Size = new Size(180, 20) }; y += 40;

            Label lblEmail = new Label() { Text = "Email:", Location = new Point(10, y) };
            txtEmail = new TextBox() { Location = new Point(110, y), Size = new Size(180, 20) }; y += 40;

            Label lblRole = new Label() { Text = "Role:", Location = new Point(10, y) };
            cmbRole = new ComboBox() { Location = new Point(110, y), Size = new Size(180, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRole.Items.AddRange(new string[] { "SuperAdmin", "Admin", "Manager", "Customer" }); y += 40;

            Label lblPhone = new Label() { Text = "Phone:", Location = new Point(10, y) };
            txtPhone = new TextBox() { Location = new Point(110, y), Size = new Size(180, 20) }; y += 40;

            Label lblAddr = new Label() { Text = "Address:", Location = new Point(10, y) };
            txtAddress = new TextBox() { Location = new Point(110, y), Size = new Size(180, 20) }; y += 40;

            Label lblSalary = new Label() { Text = "Salary:", Location = new Point(10, y) };
            txtSalary = new TextBox() { Location = new Point(110, y), Size = new Size(180, 20) }; y += 60;

            btnAdd = new Button() { Text = "Add", Location = new Point(10, y), Size = new Size(70, 30) };
            btnUpdate = new Button() { Text = "Update", Location = new Point(85, y), Size = new Size(70, 30) };
            btnDelete = new Button() { Text = "Delete", Location = new Point(160, y), Size = new Size(70, 30) };
            btnClear = new Button() { Text = "Clear", Location = new Point(235, y), Size = new Size(70, 30) };

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ClearFields();

            inputPanel.Controls.AddRange(new Control[] { lblName, txtName, lblEmail, txtEmail, lblRole, cmbRole, lblPhone, txtPhone, lblAddr, txtAddress, lblSalary, txtSalary, btnAdd, btnUpdate, btnDelete, btnClear });

            this.Controls.Add(dgvUsers);
            this.Controls.Add(inputPanel);
        }

        private void LoadUsers()
        {
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = _userRepo.GetAllUsers();
        }

        private void DgvUsers_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var user = dgvUsers.SelectedRows[0].DataBoundItem as UserBase;
                if (user != null)
                {
                    txtName.Text = user.UName;
                    txtEmail.Text = user.Email;
                    txtPhone.Text = user.UPhoneNo;
                    txtAddress.Text = user.UAddress;
                    cmbRole.SelectedItem = user.Role;
                    
                    if (user is SuperAdmin sa) txtSalary.Text = sa.Salary.ToString();
                    else if (user is Admin a) txtSalary.Text = a.Salary.ToString();
                    else if (user is Manager m) txtSalary.Text = m.Salary.ToString();
                    else txtSalary.Text = "0";
                }
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            UserBase user = CreateUserFromFields();
            if (_userRepo.AddUser(user))
            {
                MessageBox.Show("User Added Successfully");
                LoadUsers();
                ClearFields();
            }
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
             if (dgvUsers.SelectedRows.Count > 0)
             {
                var user = CreateUserFromFields();
                var selectedUser = dgvUsers.SelectedRows[0].DataBoundItem as UserBase;
                if (selectedUser != null)
                {
                    user.UId = selectedUser.UId;
                    if (_userRepo.UpdateUser(user))
                    {
                        MessageBox.Show("User Updated Successfully");
                        LoadUsers();
                    }
                }
             }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var selectedUser = dgvUsers.SelectedRows[0].DataBoundItem as UserBase;
                if (selectedUser != null)
                {
                    int id = selectedUser.UId;
                    if (_userRepo.DeleteUser(id))
                    {
                        MessageBox.Show("User Deleted Successfully");
                        LoadUsers();
                        ClearFields();
                    }
                }
            }
        }

        private UserBase CreateUserFromFields()
        {
            UserBase user;
            string role = cmbRole.SelectedItem?.ToString() ?? "Customer";
            decimal salary = decimal.TryParse(txtSalary.Text, out decimal s) ? s : 0;

            switch (role)
            {
                case "SuperAdmin": user = new SuperAdmin { Salary = salary }; break;
                case "Admin": user = new Admin { Salary = salary }; break;
                case "Manager": user = new Manager { Salary = salary }; break;
                default: user = new Customer(); break;
            }

            user.UName = txtName.Text;
            user.Email = txtEmail.Text;
            user.UPhoneNo = txtPhone.Text;
            user.UAddress = txtAddress.Text;
            user.UPassword = "password123"; // Default for new users
            return user;
        }

        private void ClearFields()
        {
            txtName.Clear(); txtEmail.Clear(); txtPhone.Clear(); txtAddress.Clear(); txtSalary.Clear();
            cmbRole.SelectedIndex = -1;
        }
    }
}
