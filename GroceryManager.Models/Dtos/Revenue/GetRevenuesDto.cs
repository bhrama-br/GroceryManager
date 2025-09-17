using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Models.Dtos.Revenue
{
    public class GetRevenuesDto
    {
        public int Id { get; set; }
        public int ApiId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public string PreparationMode { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<string> IngredientNames { get; set; } = new List<string>();
    }
}