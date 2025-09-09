using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Models.Dtos.ShoppingList
{
    public class UpdateShoppingListDto
    {
        public int Id { get; set; }
        public bool IsPurchased { get; set; } = false;
    }
}