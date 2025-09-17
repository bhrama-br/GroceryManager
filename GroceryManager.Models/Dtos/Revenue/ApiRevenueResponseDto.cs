using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Models.Dtos.Revenue
{
    public class ApiRevenueResponseDto
    {
        public List<ApiRevenueItem> Items { get; set; } = new();
        public MetaApi Meta { get; set; } = new();
    }

    public class ApiRevenueItem
    {
        public int Id { get; set; }
        public string Receita { get; set; } = string.Empty;
        public string Ingredientes { get; set; } = string.Empty;
        public string Modo_Preparo { get; set; } = string.Empty;
        public string Link_Imagem { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public List<ApiIngredientsBase> IngredientesBase { get; set; } = new();
    }

    public class ApiIngredientsBase
    {
        public int Id { get; set; }
        public List<string> NomesIngrediente { get; set; } = new();
        public int Receita_Id { get; set; }
    }

    public class MetaApi
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
    }
}