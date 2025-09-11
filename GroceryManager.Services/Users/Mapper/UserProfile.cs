using AutoMapper;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.User;

namespace GroceryManager.Services.Users.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserRegisterDto, User>();
        }
    }
}
