using FluentValidation;
using Grievance_Management_System.Models;

namespace Grievance_Management_System.Validators
{
    public class StateValidator : AbstractValidator<State>
    {
        public StateValidator()
        {
            RuleFor(s => s.StateName)
                .NotEmpty().WithMessage("State Name is required.")
                .MaximumLength(100).WithMessage("State Name must not exceed 100 characters.");

            RuleFor(s => s.StateCode)
                .MaximumLength(10).WithMessage("State Code must not exceed 10 characters.")
                .When(s => !string.IsNullOrEmpty(s.StateCode));

            RuleFor(s => s.CountryId)
                .NotEmpty().WithMessage("Country ID is required.")
                .GreaterThan(0).WithMessage("Country ID must be a valid value.");
        }
    }
}
