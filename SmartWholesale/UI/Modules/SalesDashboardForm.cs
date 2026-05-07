using System;
using System.Drawing;
using System.Windows.Forms;
using SmartWholesale.DAL;
using SmartWholesale.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartWholesale.UI.Modules
{
    public class SalesDashboardForm : Form
    {
        private Admin _owner;
        private TransactionRepository _transRepo;
        private DataGridView dgvSales = null!;
        private Label lblTotalEarnings = null!, lblTotalSales = null!, lblAvgOrder = null!;

        public SalesDashboardForm(Admin owner)
        {
            _owner = owner;
            _transRepo = new TransactionRepository();
            InitializeComponent();
            LoadSalesData();
        }

        private void InitializeComponent()
        {
            this.Text = "Sales & Earnings Dashboard - SmartWholesale";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Stats Panel
            Panel statsPanel = new Panel() { Dock = DockStyle.Top, Height = 120, BackColor = Color.WhiteSmoke };
            lblTotalEarnings = CreateStatLabel("Total Earnings: $0", new Point(50, 40), Color.Green);
            lblTotalSales = CreateStatLabel("Total Sales: 0", new Point(350, 40), Color.Blue);
            lblAvgOrder = CreateStatLabel("Avg Order Value: $0", new Point(650, 40), Color.Purple);
            statsPanel.Controls.AddRange(new Control[] { lblTotalEarnings, lblTotalSales, lblAvgOrder });

            // Sales Table
            dgvSales = new DataGridView() { Dock = DockStyle.Fill, AutoGenerateColumns = true, ReadOnly = true, AllowUserToAddRows = false };

            Label lblTableTitle = new Label() { Text = "Recent Sales Transactions", Font = new Font("Arial", 12, FontStyle.Bold), Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) };

            this.Controls.Add(dgvSales);
            this.Controls.Add(lblTableTitle);
            this.Controls.Add(statsPanel);
        }

        private Label CreateStatLabel(string text, Point loc, Color color)
        {
            return new Label() { Text = text, Location = loc, Size = new Size(250, 40), Font = new Font("Arial", 14, FontStyle.Bold), ForeColor = color };
        }

        private void LoadSalesData()
        {
            // Note: In a real app, we would filter transactions by items owned by this Admin.
            // For this course project, we show the transactions processed.
            var allTrans = _transRepo.GetAllTransactions();
            dgvSales.DataSource = allTrans;

            if (allTrans != null && allTrans.Count > 0)
            {
                decimal total = allTrans.Sum(t => t.TotalAmount);
                lblTotalEarnings.Text = $"Total Earnings: ${total:N2}";
                lblTotalSales.Text = $"Total Sales: {allTrans.Count}";
                lblAvgOrder.Text = $"Avg Order Value: ${ (total / allTrans.Count):N2}";
            }
        }
    }
}
