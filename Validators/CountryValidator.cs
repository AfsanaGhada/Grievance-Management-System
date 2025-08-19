using FluentValidation;
using Grievance_Management_System.Models;


namespace Grievance_Management_System.Validators
{
    public class CountryValidator : AbstractValidator<Country>
    {
        public CountryValidator()
        {
            RuleFor(c => c.CountryName)
                .NotNull().WithMessage("Country name must not be empty.")
                .Length(3, 20).WithMessage("Country name must be between 3 and 20 characters.")
                .Matches("^[A-Za-z ]*$").WithMessage("Country name must contain only letters and spaces.");

            
        }
    }
}
