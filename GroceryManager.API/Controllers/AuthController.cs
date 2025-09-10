using Microsoft.AspNetCore.Mvc;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.User;

namespace GroceryManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<string> Login([FromBody] UserLoginDto user, CancellationToken cancellationToken)
        {
            return await _authService.Login(user, cancellationToken);
        }

        [HttpPost("register")]
        public async Task<string> Register([FromBody] UserRegisterDto user, CancellationToken cancellationToken)
        {
            return await _authService.Register(user, cancellationToken);
        }
    }
}