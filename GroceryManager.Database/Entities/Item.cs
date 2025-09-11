using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Database.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public List<string> Names { get; set; } = new List<string>();
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsPurchased { get; set; } = false;
        public Supermarket Supermarket { get; set; }
        public ShoppingList? ShoppingList { get; set; }
    }
}