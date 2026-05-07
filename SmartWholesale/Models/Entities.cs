using System;
using System.Collections.Generic;

namespace SmartWholesale.Models
{
    public class Item
    {
        public int IId { get; set; }
        public string IType { get; set; }
        public string IBrand { get; set; }
        public string IModelNo { get; set; }
        public decimal IPrice { get; set; }
        public int IStockStatus { get; set; }
        public int IMinimumStock { get; set; }
        public int OwnerId { get; set; } // FK to Users
    }

    public class Bill
    {
        public int BillId { get; set; }
        public DateTime BillDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public List<BillItem> Items { get; set; } = new List<BillItem>();
    }

    public class BillItem
    {
        public int BillItemId { get; set; }
        public int BillId { get; set; }
        public int IId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ItemName { get; set; } // Helpful for UI
    }

    public class Transaction
    {
        public int TId { get; set; }
        public int BillId { get; set; }
        public int UId { get; set; } // Processor
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class CartItem
    {
        public int CartId { get; set; }
        public int UId { get; set; }
        public int IId { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
    }

    public class Review
    {
        public int ReviewId { get; set; }
        public int UId { get; set; }
        public int IId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public string UserName { get; set; } // For display
    }

    public class Offer
    {
        public int OfferId { get; set; }
        public int IId { get; set; }
        public double DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
