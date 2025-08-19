using FluentValidation;
using Grievance_Management_System.Models;

namespace Grievance_Management_System.Validators
{
    public class FacilityValidator : AbstractValidator<Facility>
    {
        public FacilityValidator()
        {
            RuleFor(f => f.FacilityName)
                .NotEmpty().WithMessage("Facility Name is required.")
                .MaximumLength(100).WithMessage("Facility Name must not exceed 100 characters.");

            RuleFor(f => f.FacilityTypeId)
                .NotEmpty().WithMessage("Facility Type is required.")
                .GreaterThan(0).WithMessage("Facility Type must be a valid selection.");

            RuleFor(f => f.Location)
                .MaximumLength(200).WithMessage("Location must not exceed 200 characters.")
                .When(f => !string.IsNullOrEmpty(f.Location));

            RuleFor(f => f.CityId)
                .GreaterThan(0).WithMessage("City must be a valid selection.")
                .When(f => f.CityId.HasValue);
        }
    }
}
