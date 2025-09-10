using FluentValidation;
using GroceryManager.Models.Dtos;
using GroceryManager.Models.Dtos.User;

namespace GroceryManager.Services.Users.Validators
{
    public class UserDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserDtoValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
