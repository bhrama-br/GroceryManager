using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Database.Entities
{
    public class Supermarket
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Item>? Items { get; set; }
    }
}