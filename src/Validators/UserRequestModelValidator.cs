using FluentValidation;
using OneByte.Contracts.RequestModels;

namespace OneByte.Validators
{
    public class UserRequestModelValidator : AbstractValidator<UserRequestModel>
    {
        public UserRequestModelValidator()
        {
            RuleFor(x => x.Username)
            .Matches("^[a-zA-Z0-9]*$")
            .WithMessage("Username must be alphanumeric")
            .NotNull()
            .WithMessage("Username is required")
            .NotEmpty()
            .WithMessage("Name is required");
            

            RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Password is required")
            .NotEmpty()
            .WithMessage("Password is required");
        }
    }
}