using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.Models
{
    public class Cart
    {

        public void Clear()
        {
            Items.Clear();
        }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal TotalPrice => Items.Sum(item => item.Quantity * item.Product.Price);

        public void AddItem(Product product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem { ProductId = product.Id, Quantity = quantity, Product = product });
            }
        }

        public void RemoveItem(int productId)
        {
            var itemToRemove = Items.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
            }
        }

        public void UpdateItemQuantity(int productId, int newQuantity)
        {
            var itemToUpdate = Items.FirstOrDefault(i => i.ProductId == productId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = newQuantity;
            }
        }

    }
}
