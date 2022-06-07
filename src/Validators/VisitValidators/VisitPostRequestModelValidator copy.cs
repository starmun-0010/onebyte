using FluentValidation;
using OneByte.Contracts.RequestModels.Visit;

namespace OneByte.Validators.PatientValidators
{
    public class VisitPostRequestModelValidator : AbstractValidator<VisitPostRequestModel>
    {
        public VisitPostRequestModelValidator()
        {
            RuleFor(x => x.PatientID).NotNull().WithMessage("Patient ID is required");
            RuleFor(x => x.DoctorID).NotNull().WithMessage("Doctor ID is required");
            RuleFor(x => x.VisitDate).NotNull().WithMessage("Visit date is required");
        }
    }
}