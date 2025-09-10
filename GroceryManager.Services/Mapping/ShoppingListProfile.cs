using AutoMapper;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.ShoppingList;

namespace GroceryManager.Services.Mapping
{
    public class ShoppingListProfile : Profile
    {
        public ShoppingListProfile()
        {
            CreateMap<ShoppingList, GetShoppingListDto>();
            CreateMap<UpdateShoppingListDto, ShoppingList>();
        }
    }
}
