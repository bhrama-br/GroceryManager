using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.Item;

namespace GroceryManager.Models.Dtos.Supermarket
{
    public class GetSupermarketDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GetItemDto>? Items { get; set; }
    }
}