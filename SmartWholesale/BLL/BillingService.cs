using System.Collections.Generic;
using SmartWholesale.DAL;
using SmartWholesale.Models;

namespace SmartWholesale.BLL
{
    public class BillingService
    {
        private readonly CartRepository _cartRepo;
        private readonly TransactionRepository _transRepo;

        public BillingService()
        {
            _cartRepo = new CartRepository();
            _transRepo = new TransactionRepository();
        }

        public List<CartItem> GetCart(int userId)
        {
            return _cartRepo.GetCartItems(userId);
        }

        public bool AddToCart(int userId, int itemId, int qty)
        {
            return _cartRepo.AddToCart(userId, itemId, qty);
        }

        public bool RemoveFromCart(int cartId)
        {
            return _cartRepo.RemoveFromCart(cartId);
        }

        public decimal CalculateBulkDiscount(int quantity, decimal unitPrice)
        {
            decimal total = quantity * unitPrice;
            if (quantity >= 50) return total * 0.15m; // 15% discount for 50+ items
            if (quantity >= 20) return total * 0.10m; // 10% discount for 20+ items
            if (quantity >= 10) return total * 0.05m; // 5% discount for 10+ items
            return 0;
        }

        public bool ProcessPurchase(int userId, List<CartItem> cartItems)
        {
            if (cartItems.Count == 0) return false;

            decimal subtotal = 0;
            decimal totalDiscount = 0;
            Bill bill = new Bill { CustomerId = userId };

            foreach (var ci in cartItems)
            {
                bill.Items.Add(new BillItem
                {
                    IId = ci.IId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Price
                });
                subtotal += ci.Price * ci.Quantity;
                totalDiscount += CalculateBulkDiscount(ci.Quantity, ci.Price);
            }

            bill.TotalAmount = subtotal - totalDiscount;

            int billId = _transRepo.CreateBill(bill);
            if (billId > 0)
            {
                // Record Transaction
                _transRepo.RecordTransaction(new Transaction
                {
                    BillId = billId,
                    UId = userId,
                    TotalAmount = bill.TotalAmount
                });

                // Clear Cart
                _cartRepo.ClearCart(userId);
                return true;
            }

            return false;
        }
    }
}
