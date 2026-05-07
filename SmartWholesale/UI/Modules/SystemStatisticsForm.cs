using System;
using System.Drawing;
using System.Windows.Forms;
using SmartWholesale.DAL;

namespace SmartWholesale.UI.Modules
{
    public class SystemStatisticsForm : Form
    {
        public SystemStatisticsForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "System Statistics - SmartWholesale";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label() { Text = "Overall System Statistics", Font = new Font("Arial", 16, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };

            UserRepository uRepo = new UserRepository();
            ItemRepository iRepo = new ItemRepository();
            TransactionRepository tRepo = new TransactionRepository();

            int totalUsers = uRepo.GetAllUsers().Count;
            int totalItems = iRepo.GetAllItems().Count;
            int totalTrans = tRepo.GetAllTransactions().Count;

            Label lblUsers = new Label() { Text = $"Total Registered Users: {totalUsers}", Font = new Font("Arial", 12), Location = new Point(50, 80), AutoSize = true };
            Label lblItems = new Label() { Text = $"Total Inventory Items: {totalItems}", Font = new Font("Arial", 12), Location = new Point(50, 130), AutoSize = true };
            Label lblTrans = new Label() { Text = $"Total Transactions: {totalTrans}", Font = new Font("Arial", 12), Location = new Point(50, 180), AutoSize = true };

            this.Controls.AddRange(new Control[] { lblTitle, lblUsers, lblItems, lblTrans });
        }
    }
}
