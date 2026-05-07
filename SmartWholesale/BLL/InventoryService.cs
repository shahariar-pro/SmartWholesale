using System.Collections.Generic;
using SmartWholesale.DAL;
using SmartWholesale.Models;

namespace SmartWholesale.BLL
{
    public class InventoryService
    {
        private readonly ItemRepository _itemRepo;

        public InventoryService()
        {
            _itemRepo = new ItemRepository();
        }

        public List<Item> GetAvailableProducts()
        {
            return _itemRepo.GetAllItems();
        }

        public List<Item> GetMyProducts(int ownerId)
        {
            return _itemRepo.GetItemsByOwner(ownerId);
        }

        public bool AddProduct(Item item)
        {
            if (item.IPrice <= 0 || item.IStockStatus < 0)
                return false;

            return _itemRepo.AddItem(item);
        }

        public bool UpdateProduct(Item item)
        {
            return _itemRepo.UpdateItem(item);
        }

        public bool RemoveProduct(int id)
        {
            return _itemRepo.DeleteItem(id);
        }
    }
}
