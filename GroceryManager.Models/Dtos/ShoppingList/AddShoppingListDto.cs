using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Models.Dtos.ShoppingList
{
    public class AddShoppingListDto
    {
        public bool IsPurchased { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}