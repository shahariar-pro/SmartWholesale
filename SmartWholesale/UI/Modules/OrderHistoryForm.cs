using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartWholesale.DAL;
using SmartWholesale.Models;

namespace SmartWholesale.UI.Modules
{
    public class OrderHistoryForm : Form
    {
        private Customer _user;
        private TransactionRepository _transRepo;
        private DataGridView dgvOrders = null!;

        public OrderHistoryForm(Customer user)
        {
            _user = user;
            _transRepo = new TransactionRepository();
            InitializeComponent();
            LoadOrders();
        }

        private void InitializeComponent()
        {
            this.Text = "My Order History - SmartWholesale";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel topPanel = new Panel() { Dock = DockStyle.Top, Height = 60, BackColor = Color.WhiteSmoke };
            Label lblTitle = new Label() { Text = "My Orders", Font = new Font("Arial", 16, FontStyle.Bold), Location = new Point(20, 15), AutoSize = true };
            topPanel.Controls.Add(lblTitle);

            dgvOrders = new DataGridView() { Dock = DockStyle.Fill, AutoGenerateColumns = true, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

            this.Controls.Add(dgvOrders);
            this.Controls.Add(topPanel);
        }

        private void LoadOrders()
        {
            var myOrders = _transRepo.GetAllTransactions().Where(t => t.UId == _user.UId).ToList();
            dgvOrders.DataSource = myOrders;
        }
    }
}
