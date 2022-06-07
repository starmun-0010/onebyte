using FluentValidation;
using OneByte.Contracts.RequestModels.Patient;

namespace OneByte.Validators.DoctorValidators
{
    public class PatientPostRequestModelValidator : AbstractValidator<PatientPostRequestModel>
    {
        public PatientPostRequestModelValidator()
        {
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