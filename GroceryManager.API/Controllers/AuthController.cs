using Microsoft.AspNetCore.Mvc;
using GroceryManager.Auth.Services;
using GroceryManager.Database.Entities;
using GroceryManager.Models.Dtos.User;

namespace GroceryManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDto request)
        {
            var response = await _tokenService.Login(
                request.Username,
                request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var response = await _tokenService.Register(
                new User { Username = request.Username },
                request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}