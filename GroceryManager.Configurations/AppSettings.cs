using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Configurations
{
    public class AppSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public JwtSettings Jwt { get; set; } = new();
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}