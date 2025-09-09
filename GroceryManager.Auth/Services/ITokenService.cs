using GroceryManager.Database.Entities;
using GroceryManager.Models;

namespace GroceryManager.Auth.Services
{
    public interface ITokenService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}