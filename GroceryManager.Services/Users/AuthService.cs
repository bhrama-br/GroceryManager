using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GroceryManager.Auth.Models;
using GroceryManager.Database.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using GroceryManager.Database;
using GroceryManager.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using GroceryManager.Models.Dtos.User;
using AutoMapper;

namespace GroceryManager.Services.Users
{
    public interface IAuthService
    {
        Task<string> Register(UserRegisterDto dto, CancellationToken cancellationToken);
        Task<string> Login(UserLoginDto loginDto, CancellationToken cancellationToken);
        Task<bool> UserExists(string username);
    }

    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IValidator<UserLoginDto> _loginValidator;
        private readonly IValidator<UserRegisterDto> _registerValidator;

        public AuthService(
            DataContext context,
            IMapper mapper,
            IOptions<JwtSettings> jwtSettings,
            IValidator<UserLoginDto> loginValidator,
            IValidator<UserRegisterDto> registerValidator)
        {
            _jwtSettings = jwtSettings.Value;
            _context = context;
            _mapper = mapper;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        public async Task<string> Login(UserLoginDto loginDto, CancellationToken cancellationToken)
        {
            var validation = await _loginValidator.ValidateAsync(loginDto, cancellationToken);
            if (!validation.IsValid)
                return string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == loginDto.Username.ToLower(), cancellationToken);

            if (user is null)
                return "User not found.";

            if (!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
                return "Wrong password.";

            return CreateToken(user);
        }

        public async Task<string> Register(UserRegisterDto dto, CancellationToken cancellationToken)
        {
            var validation = await _registerValidator.ValidateAsync(dto, cancellationToken);
            if (!validation.IsValid)
                return string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));

            if (await _context.Users.AnyAsync(u => u.Username == dto.Username, cancellationToken))
                return "User already exists.";

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return "User registered successfully.";
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }

            return false;
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
