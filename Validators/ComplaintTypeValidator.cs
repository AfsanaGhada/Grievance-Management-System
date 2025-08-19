
using FluentValidation;
using Grievance_Management_System.Models;

namespace Grievance_Management_System.Validators
{
    public class ComplaintTypeValidator : AbstractValidator<ComplaintType>
    {
        public ComplaintTypeValidator()
        {
            RuleFor(x => x.ComplaintName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(100).WithMessage("Full Name must not exceed 100 characters.");
        }
    }
}

