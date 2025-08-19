using FluentValidation;
using Grievance_Management_System.Models;

namespace Grievance_Management_System.Validators
{
    public class AdminValidator : AbstractValidator<Admin>
    {
        public AdminValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("FullName is required.");

            RuleFor(x => x.FullName)
                .MaximumLength(100)
                .WithMessage("FullName cannot exceed 100 characters.");

            RuleFor(x => x.Phone)
                .Matches(@"^\d{10}$")
                .WithMessage("Phone must be a valid 10-digit number.");
        }
    }
}
