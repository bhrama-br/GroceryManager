using GroceryManager.Database.Entities;
using GroceryManager.Models;

namespace GroceryManager.Auth.Services
{
    public interface ITokenService
    {
        Task<string> Register(User user, string password);
        Task<string> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}