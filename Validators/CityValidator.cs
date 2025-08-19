using FluentValidation;
using Grievance_Management_System.Models;

namespace Grievance_Management_System.Validators
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(c => c.CityName)
                .NotEmpty().WithMessage("City Name is required.")
                .MaximumLength(100).WithMessage("City Name must not exceed 100 characters.");

            RuleFor(c => c.CityCode)
                .MaximumLength(20).WithMessage("City Code must not exceed 20 characters.")
                .When(c => !string.IsNullOrEmpty(c.CityCode));

            RuleFor(c => c.StateId)
                .NotEmpty().WithMessage("State selection is required.")
                .GreaterThan(0).WithMessage("Please select a valid state.");
        }
    }
}
