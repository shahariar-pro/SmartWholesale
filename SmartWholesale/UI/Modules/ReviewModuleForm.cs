using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartWholesale.DAL;
using SmartWholesale.Models;
using System.Collections.Generic;

namespace SmartWholesale.UI.Modules
{
    public class ReviewModuleForm : Form
    {
        private UserBase _user;
        private ReviewRepository _reviewRepo;
        private ItemRepository _itemRepo;
        private ComboBox cmbItems = null!;
        private NumericUpDown numRating = null!;
        private TextBox txtComment = null!;

        public ReviewModuleForm(UserBase user)
        {
            _user = user;
            _reviewRepo = new ReviewRepository();
            _itemRepo = new ItemRepository();
            InitializeComponent();
            LoadOrderedItems();
        }

        private void InitializeComponent()
        {
            this.Text = "Reviews - SmartWholesale";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label() { Text = "Rate Your Purchase", Font = new Font("Arial", 16, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };

            Label lblItem = new Label() { Text = "Select Product:", Location = new Point(20, 70), AutoSize = true };
            cmbItems = new ComboBox() { Location = new Point(150, 65), Size = new Size(250, 25), DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblRating = new Label() { Text = "Rating (1-5 Stars):", Location = new Point(20, 110), AutoSize = true };
            numRating = new NumericUpDown() { Location = new Point(150, 105), Minimum = 1, Maximum = 5, Value = 5, Size = new Size(60, 25) };

            Label lblComment = new Label() { Text = "Your Review:", Location = new Point(20, 150), AutoSize = true };
            txtComment = new TextBox() { Location = new Point(150, 145), Size = new Size(250, 100), Multiline = true };

            Button btnSubmit = new Button() { Text = "Submit Review", Location = new Point(150, 260), Size = new Size(150, 40), BackColor = Color.Blue, ForeColor = Color.White };
            btnSubmit.Click += BtnSubmit_Click;

            this.Controls.AddRange(new Control[] { lblTitle, lblItem, cmbItems, lblRating, numRating, lblComment, txtComment, btnSubmit });
        }

        private void LoadOrderedItems()
        {
            var orderedItemIds = _reviewRepo.GetOrderedItemIds(_user.UId);
            var allItems = _itemRepo.GetAllItems();
            
            var myOrderedItems = allItems.Where(i => orderedItemIds.Contains(i.IId)).ToList();
            
            cmbItems.DataSource = myOrderedItems;
            cmbItems.DisplayMember = "IModelNo"; // Shows model number
            cmbItems.ValueMember = "IId";

            if (myOrderedItems.Count == 0)
            {
                MessageBox.Show("You haven't ordered any products yet. Please purchase something first to leave a review!", "Info");
                this.Close();
            }
        }

        private void BtnSubmit_Click(object? sender, EventArgs e)
        {
            if (cmbItems.SelectedValue == null) return;

            Review review = new Review
            {
                UId = _user.UId,
                IId = (int)cmbItems.SelectedValue,
                Rating = (int)numRating.Value,
                Comment = txtComment.Text,
                ReviewDate = DateTime.Now
            };

            if (_reviewRepo.AddReview(review))
            {
                MessageBox.Show("Review submitted successfully! Thank you for your feedback.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to submit review.", "Error");
            }
        }
    }
}
