using FluentValidation;
using OneByte.Contracts.RequestModels.Doctor;

namespace OneByte.Validators.DoctorValidators
{
    public class DoctorPutRequestModelValidator : AbstractValidator<DoctorPutRequestModel>
    {
        public DoctorPutRequestModelValidator()
        {
            RuleFor(x => x.ID)
            .NotNull()
            .WithMessage("ID is required");

            RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("Name is required")
            .NotEmpty()
            .WithMessage("Name is required")
            .Matches("^[a-zA-Z ]*$")
            .WithMessage("Name must be alphabetic and contain only letters");

            RuleFor(x => x.Contact)
            .NotNull()
            .WithMessage("Contact is required")
            .NotEmpty()
            .WithMessage("Contact is required")
            .Matches("^[0-9]*$")
            .WithMessage("Contact must be numeric");
        }
    }
}