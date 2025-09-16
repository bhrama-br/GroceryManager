using AutoMapper;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.ShoppingList;

namespace GroceryManager.Services.ShoppingLists.Mapper
{
    public class ShoppingListProfile : Profile
    {
        public ShoppingListProfile()
        {
            CreateMap<ShoppingList, GetShoppingListDto>();
            CreateMap<AddShoppingListDto, ShoppingList>();
            CreateMap<UpdateShoppingListDto, ShoppingList>();
        }
    }
}
