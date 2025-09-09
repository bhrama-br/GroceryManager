using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryManager.Models.Dtos.Item;

namespace GroceryManager.Models.Dtos.ShoppingList
{
    public class GetShoppingListDto
    {
        public int Id { get; set; }
        public bool IsPurchased { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<GetItemDto>? Items { get; set; }
    }
}