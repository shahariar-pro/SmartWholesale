using System;
using System.Drawing;
using System.Windows.Forms;
using SmartWholesale.BLL;
using SmartWholesale.Models;
using System.Collections.Generic;

namespace SmartWholesale.UI.Modules
{
    public class ProductManagementForm : Form
    {
        private DataGridView dgvProducts = null!;
        private TextBox txtType = null!, txtBrand = null!, txtModel = null!, txtPrice = null!, txtStock = null!, txtMinStock = null!;
        private Button btnAdd = null!, btnUpdate = null!, btnDelete = null!, btnClear = null!, btnAddToCart = null!;
        private InventoryService _inventoryService;
        private UserBase _currentUser;

        public ProductManagementForm(UserBase user)
        {
            _currentUser = user;
            _inventoryService = new InventoryService();
            InitializeComponent();
            LoadProducts();
            SetupPermissions();
        }

        private void InitializeComponent()
        {
            this.Text = "Product & Inventory Management - SmartWholesale";
            this.Size = new Size(1100, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvProducts = new DataGridView() { Location = new Point(20, 20), Size = new Size(700, 500), AutoGenerateColumns = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, ReadOnly = true };
            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;

            Panel inputPanel = new Panel() { Location = new Point(740, 20), Size = new Size(320, 500), BorderStyle = BorderStyle.FixedSingle };
            
            int y = 20;
            Label lblType = new Label() { Text = "Type:", Location = new Point(10, y) };
            txtType = new TextBox() { Location = new Point(110, y), Size = new Size(180, 20) }; y += 40;

            Label lblBrand = new Label() { Text = "Brand:", Location = new Point(10, y) };
            txtBrand = new TextBox() { Location = new Point(110, y), Size = new Size(180, 20) }; y += 40;

            Label lblModel = new Label() { Text = "Model No:", Location = new Point(10, y) };
            txtModel = new TextBox() { Location = new Point(110, y), Size = new Size(180, 20) }; y += 40;

            Label lblPrice = new Label() { Text = "Price:", Location = new Point(10, y) };
            txtPrice = new TextBox() { Location = new Point(110, y), Size = new Size(180, 20) }; y += 40;

            Label lblStock = new Label() { Text = "Stock:", Location = new Point(10, y) };
            txtStock = new TextBox() { Location = new Point(110, y), Size = new Size(180, 20) }; y += 40;

            Label lblMinStock = new Label() { Text = "Min Stock:", Location = new Point(10, y) };
            txtMinStock = new TextBox() { Location = new Point(110, y), Size = new Size(180, 20) }; y += 60;

            btnAdd = new Button() { Text = "Add", Location = new Point(10, y), Size = new Size(70, 30) };
            btnUpdate = new Button() { Text = "Update", Location = new Point(85, y), Size = new Size(70, 30) };
            btnDelete = new Button() { Text = "Delete", Location = new Point(160, y), Size = new Size(70, 30) };
            btnClear = new Button() { Text = "Clear", Location = new Point(235, y), Size = new Size(70, 30) };

            btnAddToCart = new Button() { Text = "Add to Cart", Location = new Point(10, y + 40), Size = new Size(295, 30), BackColor = Color.Blue, ForeColor = Color.White, Visible = false };

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ClearFields();
            btnAddToCart.Click += BtnAddToCart_Click;

            inputPanel.Controls.AddRange(new Control[] { lblType, txtType, lblBrand, txtBrand, lblModel, txtModel, lblPrice, txtPrice, lblStock, txtStock, lblMinStock, txtMinStock, btnAdd, btnUpdate, btnDelete, btnClear, btnAddToCart });

            this.Controls.Add(dgvProducts);
            this.Controls.Add(inputPanel);
        }

        private void SetupPermissions()
        {
            if (_currentUser is Customer)
            {
                btnAdd.Enabled = btnUpdate.Enabled = btnDelete.Enabled = false;
                txtType.ReadOnly = txtBrand.ReadOnly = txtModel.ReadOnly = txtPrice.ReadOnly = txtStock.ReadOnly = txtMinStock.ReadOnly = true;
                this.Text = "Browse Products - SmartWholesale";
                btnAddToCart.Visible = true;
            }
        }

        private void LoadProducts()
        {
            dgvProducts.DataSource = null;
            if (_currentUser is Admin admin)
                dgvProducts.DataSource = _inventoryService.GetMyProducts(admin.UId);
            else
                dgvProducts.DataSource = _inventoryService.GetAvailableProducts();
        }

        private void DgvProducts_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                var item = dgvProducts.SelectedRows[0].DataBoundItem as Item;
                if (item != null)
                {
                    txtType.Text = item.IType;
                    txtBrand.Text = item.IBrand;
                    txtModel.Text = item.IModelNo;
                    txtPrice.Text = item.IPrice.ToString();
                    txtStock.Text = item.IStockStatus.ToString();
                    txtMinStock.Text = item.IMinimumStock.ToString();
                }
            }
        }

        private void BtnAddToCart_Click(object? sender, EventArgs e)
        {
             if (dgvProducts.SelectedRows.Count > 0)
             {
                 var item = dgvProducts.SelectedRows[0].DataBoundItem as Item;
                 if (item != null)
                 {
                     BillingService bs = new BillingService();
                     if (bs.AddToCart(_currentUser.UId, item.IId, 1))
                     {
                         MessageBox.Show("Item added to cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     }
                     else
                     {
                         MessageBox.Show("Failed to add item to cart.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 }
             }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            Item item = CreateItemFromFields();
            
            // Fix: Ensure OwnerId is set to a valid UId from Users table
            // For both Admin and Manager, we use their current UId as the owner/manager record
            item.OwnerId = _currentUser.UId;
            
            if (_inventoryService.AddProduct(item))
            {
                MessageBox.Show("Product Added Successfully");
                LoadProducts();
                ClearFields();
            }
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
             if (dgvProducts.SelectedRows.Count > 0)
             {
                var item = CreateItemFromFields();
                var selectedItem = dgvProducts.SelectedRows[0].DataBoundItem as Item;
                if (selectedItem != null)
                {
                    item.IId = selectedItem.IId;
                    item.OwnerId = selectedItem.OwnerId;

                    if (_inventoryService.UpdateProduct(item))
                    {
                        MessageBox.Show("Product Updated Successfully");
                        LoadProducts();
                    }
                }
             }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                var selectedItem = dgvProducts.SelectedRows[0].DataBoundItem as Item;
                if (selectedItem != null)
                {
                    int id = selectedItem.IId;
                    if (_inventoryService.RemoveProduct(id))
                    {
                        MessageBox.Show("Product Deleted Successfully");
                        LoadProducts();
                        ClearFields();
                    }
                }
            }
        }

        private Item CreateItemFromFields()
        {
            return new Item
            {
                IType = txtType.Text,
                IBrand = txtBrand.Text,
                IModelNo = txtModel.Text,
                IPrice = decimal.TryParse(txtPrice.Text, out decimal p) ? p : 0,
                IStockStatus = int.TryParse(txtStock.Text, out int s) ? s : 0,
                IMinimumStock = int.TryParse(txtMinStock.Text, out int ms) ? ms : 0
            };
        }

        private void ClearFields()
        {
            txtType.Clear(); txtBrand.Clear(); txtModel.Clear(); txtPrice.Clear(); txtStock.Clear(); txtMinStock.Clear();
        }
    }
}