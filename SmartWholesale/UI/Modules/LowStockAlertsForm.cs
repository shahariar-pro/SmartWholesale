using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartWholesale.DAL;

namespace SmartWholesale.UI.Modules
{
    public class LowStockAlertsForm : Form
    {
        private ItemRepository _itemRepo;
        private DataGridView dgvAlerts = null!;

        public LowStockAlertsForm()
        {
            _itemRepo = new ItemRepository();
            InitializeComponent();
            LoadAlerts();
        }

        private void InitializeComponent()
        {
            this.Text = "Low Stock Alerts - SmartWholesale";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel topPanel = new Panel() { Dock = DockStyle.Top, Height = 80, BackColor = Color.MistyRose };
            Label lblTitle = new Label() { Text = "Items Below Reorder Level", Font = new Font("Arial", 14, FontStyle.Bold), Location = new Point(20, 15), AutoSize = true };
            Label lblDesc = new Label() { Text = "Alert: The following items have stock levels below their minimum stock point. Please consider restocking.", ForeColor = Color.Red, Location = new Point(20, 45), AutoSize = true };
            topPanel.Controls.AddRange(new Control[] { lblTitle, lblDesc });

            dgvAlerts = new DataGridView() { Dock = DockStyle.Fill, AutoGenerateColumns = true, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

            this.Controls.Add(dgvAlerts);
            this.Controls.Add(topPanel);
        }

        private void LoadAlerts()
        {
            var items = _itemRepo.GetAllItems().Where(i => i.IStockStatus < i.IMinimumStock).ToList();
            dgvAlerts.DataSource = items;
        }
    }
}
