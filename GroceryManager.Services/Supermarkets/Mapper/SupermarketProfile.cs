using AutoMapper;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.Supermarket;

namespace GroceryManager.Services.Supermarkets.Mapper
{
    public class SupermarketProfile : Profile
    {
        public SupermarketProfile()
        {
            CreateMap<Supermarket, GetSupermarketDto>();
            CreateMap<AddSupermarketDto, Supermarket>();
            CreateMap<UpdateSupermarketDto, Supermarket>();
        }
    }
}
