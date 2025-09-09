using AutoMapper;
using GroceryManager.Database.Entities;
using GroceryManager.Models;
using GroceryManager.Models.Dtos.Item;
using GroceryManager.Models.Dtos.ShoppingList;

namespace GroceryManager.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Item
            CreateMap<Item, GetItemDto>().ReverseMap();
            CreateMap<AddItemDto, Item>();

            // ShoppingList
            CreateMap<ShoppingList, GetShoppingListDto>().ReverseMap();
            CreateMap<AddShoppingListDto, ShoppingList>();
        }
    }
}
