using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Models.Dtos.Item
{
    public class GetItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Supermarket { get; set; }
        public int Quantity { get; set; }
        public List<string>? Names { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPurchased { get; set; }
    }
}