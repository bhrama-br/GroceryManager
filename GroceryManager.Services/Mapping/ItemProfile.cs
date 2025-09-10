using AutoMapper;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.Item;

namespace GroceryManager.Services.Mapping
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, GetItemDto>();
            CreateMap<AddItemDto, Item>();
            CreateMap<UpdateItemDto, Item>();
        }
    }
}
