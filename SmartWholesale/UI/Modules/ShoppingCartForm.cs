using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartWholesale.BLL;
using SmartWholesale.Models;

namespace SmartWholesale.UI.Modules
{
    public class ShoppingCartForm : Form
    {
        private Customer _user;
        private BillingService _billingService;
        private DataGridView dgvCart = null!;
        private Label lblSubtotal = null!, lblTotal = null!;
        private Button btnRemove = null!, btnCheckout = null!;

        public ShoppingCartForm(Customer user)
        {
            _user = user;
            _billingService = new BillingService();
            InitializeComponent();
            LoadCart();
        }

        private void InitializeComponent()
        {
            this.Text = "My Shopping Cart - SmartWholesale";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label() { Text = "My Shopping Cart", Font = new Font("Arial", 16, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };

            dgvCart = new DataGridView() { Location = new Point(20, 60), Size = new Size(740, 350), AutoGenerateColumns = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, ReadOnly = true };

            Panel summaryPanel = new Panel() { Location = new Point(500, 430), Size = new Size(260, 100), BorderStyle = BorderStyle.FixedSingle };
            lblSubtotal = new Label() { Text = "Subtotal: $0.00", Location = new Point(10, 10), AutoSize = true, Font = new Font("Arial", 10) };
            lblTotal = new Label() { Text = "Total: $0.00", Location = new Point(10, 40), AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            summaryPanel.Controls.AddRange(new Control[] { lblSubtotal, lblTotal });

            btnRemove = new Button() { Text = "Remove Item", Location = new Point(20, 430), Size = new Size(120, 40), BackColor = Color.IndianRed, ForeColor = Color.White };
            btnCheckout = new Button() { Text = "Confirm Purchase", Location = new Point(150, 430), Size = new Size(150, 40), BackColor = Color.DodgerBlue, ForeColor = Color.White };

            btnRemove.Click += BtnRemove_Click;
            btnCheckout.Click += BtnCheckout_Click;

            this.Controls.AddRange(new Control[] { lblTitle, dgvCart, summaryPanel, btnRemove, btnCheckout });
        }

        private void LoadCart()
        {
            var cartItems = _billingService.GetCart(_user.UId);
            dgvCart.DataSource = cartItems;

            if (cartItems != null && cartItems.Count > 0)
            {
                decimal total = cartItems.Sum(c => c.Price * c.Quantity);
                lblSubtotal.Text = $"Subtotal: ${total:N2}";
                lblTotal.Text = $"Total: ${total:N2}";
                btnCheckout.Enabled = true;
            }
            else
            {
                lblSubtotal.Text = "Subtotal: $0.00";
                lblTotal.Text = "Total: $0.00";
                btnCheckout.Enabled = false;
            }
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                var cartItem = dgvCart.SelectedRows[0].DataBoundItem as CartItem;
                if (cartItem != null)
                {
                    if (_billingService.RemoveFromCart(cartItem.CartId))
                    {
                        LoadCart();
                    }
                }
            }
        }

        private void BtnCheckout_Click(object? sender, EventArgs e)
        {
            var cartItems = _billingService.GetCart(_user.UId);
            if (cartItems.Count > 0)
            {
                if (_billingService.ProcessPurchase(_user.UId, cartItems))
                {
                    MessageBox.Show("Purchase Confirmed! Invoice has been generated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCart();
                }
                else
                {
                    MessageBox.Show("Failed to process purchase.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
