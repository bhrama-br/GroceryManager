using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Models.Dtos.Item
{
    public class AddItemDto
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public List<string> Names { get; set; } = new List<string>();
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsPurchased { get; set; } = false;
        public int ShoppingListId { get; set; }
        public int SupermarketId { get; set; }
    }
}