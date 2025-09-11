using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Models.Dtos.Supermarket
{
    public class UpdateSupermarketDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}