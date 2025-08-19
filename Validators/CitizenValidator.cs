using FluentValidation;
using Grievance_Management_System.Models;

namespace Grievance_Management_System.Validators
{
    public class CitizenValidator : AbstractValidator<Citizen>
    {
        public CitizenValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(100).WithMessage("Full Name must not exceed 100 characters.");

            RuleFor(x => x.Phone)
                .Matches(@"^\d{10,15}$")
                .When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Phone must contain only digits and be 10-15 digits long.");

            RuleFor(x => x.Address)
                .MaximumLength(250)
                .When(x => !string.IsNullOrEmpty(x.Address))
                .WithMessage("Address must not exceed 250 characters.");

            RuleFor(x => x.Gender)
                .Must(g => string.IsNullOrEmpty(g) || g == "Male" || g == "Female" || g == "Other")
                .WithMessage("Gender must be Male, Female, or Other.");

            RuleFor(x => x.Dob)
                .LessThan(DateOnly.FromDateTime(DateTime.Today))
                .When(x => x.Dob.HasValue)
                .WithMessage("Date of Birth must be in the past.");

            RuleFor(x => x.CityId)
                .GreaterThan(0)
                .When(x => x.CityId.HasValue)
                .WithMessage("City ID must be greater than 0.");
        }
    }
}
