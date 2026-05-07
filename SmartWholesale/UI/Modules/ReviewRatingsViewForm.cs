using System;
using System.Drawing;
using System.Windows.Forms;
using SmartWholesale.DAL;

namespace SmartWholesale.UI.Modules
{
    public class ReviewRatingsViewForm : Form
    {
        private ReviewRepository _reviewRepo;
        private DataGridView dgvReviews = null!;

        public ReviewRatingsViewForm()
        {
            _reviewRepo = new ReviewRepository();
            InitializeComponent();
            LoadReviews();
        }

        private void InitializeComponent()
        {
            this.Text = "Customer Reviews - SmartWholesale";
            this.Size = new Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label() { Text = "All Customer Ratings and Reviews", Font = new Font("Arial", 16, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };

            dgvReviews = new DataGridView() { Location = new Point(20, 70), Size = new Size(840, 350), AutoGenerateColumns = true, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

            this.Controls.AddRange(new Control[] { lblTitle, dgvReviews });
        }

        private void LoadReviews()
        {
            dgvReviews.DataSource = null;
            dgvReviews.DataSource = _reviewRepo.GetAllReviews();
        }
    }
}
