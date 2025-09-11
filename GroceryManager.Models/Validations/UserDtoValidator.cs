using FluentValidation;
using GroceryManager.Models.Dtos.User;


namespace GroceryManager.Models.Validations;

public class UserDtoValidator : AbstractValidator<UserDto>
{
  public UserDtoValidator()
  {
    RuleFor(x => x.Username)
        .NotEmpty().WithMessage("Username is required.")
        .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

    RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email is required.")
        .EmailAddress().WithMessage("Invalid email format.");

    RuleFor(x => x.Password)
        .NotEmpty().WithMessage("Password is required.")
        .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
  }
}