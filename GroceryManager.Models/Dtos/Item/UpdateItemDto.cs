using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Models.Dtos.Item
{
    public class UpdateItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public List<string> Names { get; set; } = new List<string>();
        public string Notes { get; set; } = string.Empty;
        public bool IsPurchased { get; set; } = false;
    }
}