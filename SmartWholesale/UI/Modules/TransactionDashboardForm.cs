using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartWholesale.DAL;
using SmartWholesale.Models;

namespace SmartWholesale.UI.Modules
{
    public class TransactionDashboardForm : Form
    {
        private TransactionRepository _transRepo;
        private DataGridView dgvTransactions = null!;
        private TextBox txtSearch = null!;
        private Button btnSearch = null!;

        public TransactionDashboardForm()
        {
            _transRepo = new TransactionRepository();
            InitializeComponent();
            LoadTransactions();
        }

        private void InitializeComponent()
        {
            this.Text = "Transaction Dashboard - SmartWholesale";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel topPanel = new Panel() { Dock = DockStyle.Top, Height = 60, BackColor = Color.WhiteSmoke };
            Label lblTitle = new Label() { Text = "Transactions", Font = new Font("Arial", 16, FontStyle.Bold), Location = new Point(20, 15), AutoSize = true };
            
            Label lblSearch = new Label() { Text = "Search Transaction ID:", Location = new Point(500, 20), AutoSize = true };
            txtSearch = new TextBox() { Location = new Point(640, 18), Size = new Size(150, 20) };
            btnSearch = new Button() { Text = "Search", Location = new Point(800, 16), Size = new Size(60, 25) };
            btnSearch.Click += BtnSearch_Click;

            topPanel.Controls.AddRange(new Control[] { lblTitle, lblSearch, txtSearch, btnSearch });

            dgvTransactions = new DataGridView() { Dock = DockStyle.Fill, AutoGenerateColumns = true, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

            this.Controls.Add(dgvTransactions);
            this.Controls.Add(topPanel);
        }

        private void LoadTransactions(string filterId = "")
        {
            var transactions = _transRepo.GetAllTransactions();
            if (!string.IsNullOrEmpty(filterId) && int.TryParse(filterId, out int id))
            {
                transactions = transactions.Where(t => t.TId == id).ToList();
            }
            dgvTransactions.DataSource = transactions;
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            LoadTransactions(txtSearch.Text);
        }
    }
}
