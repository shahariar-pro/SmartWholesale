using System;
using System.Drawing;
using System.Windows.Forms;
using SmartWholesale.Models;

namespace SmartWholesale.UI.Modules
{
    public class SpecialOffersForm : Form
    {
        public SpecialOffersForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Create Special Offers - SmartWholesale";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label() { Text = "Create Special Offer / Discount", Font = new Font("Arial", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };

            Label lblItem = new Label() { Text = "Item ID:", Location = new Point(20, 70), AutoSize = true };
            TextBox txtItem = new TextBox() { Location = new Point(150, 65), Size = new Size(200, 20) };

            Label lblDiscount = new Label() { Text = "Discount %:", Location = new Point(20, 110), AutoSize = true };
            NumericUpDown numDiscount = new NumericUpDown() { Location = new Point(150, 105), Minimum = 1, Maximum = 100, Value = 10, Size = new Size(200, 20) };

            Label lblStart = new Label() { Text = "Start Date:", Location = new Point(20, 150), AutoSize = true };
            DateTimePicker dtpStart = new DateTimePicker() { Location = new Point(150, 145), Size = new Size(200, 20) };

            Label lblEnd = new Label() { Text = "End Date:", Location = new Point(20, 190), AutoSize = true };
            DateTimePicker dtpEnd = new DateTimePicker() { Location = new Point(150, 185), Size = new Size(200, 20) };

            Button btnCreate = new Button() { Text = "Create Offer", Location = new Point(150, 240), Size = new Size(120, 35), BackColor = Color.Green, ForeColor = Color.White };
            btnCreate.Click += (s, e) => {
                MessageBox.Show("Special offer created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            };

            this.Controls.AddRange(new Control[] { lblTitle, lblItem, txtItem, lblDiscount, numDiscount, lblStart, dtpStart, lblEnd, dtpEnd, btnCreate });
        }
    }
}
