using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Database.Entities
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public bool IsPurchased { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Item>? Items { get; set; }
    }
}