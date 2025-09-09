using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryManager.Auth.Models;

namespace GroceryManager.Configurations
{
    public class AppSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public JwtSettings Jwt { get; set; } = new();
    }
}