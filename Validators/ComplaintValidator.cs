using FluentValidation;
using Grievance_Management_System.Models;

namespace Grievance_Management_System.Validators
{
    public class ComplaintValidator : AbstractValidator<Complaint>
    {
        public ComplaintValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(c => c.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.")
                .When(c => !string.IsNullOrEmpty(c.Description));

            RuleFor(c => c.ComplaintDate)
                .NotEmpty().WithMessage("Complaint Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Complaint Date cannot be in the future.");

            RuleFor(c => c.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(s => s == "Pending" || s == "Resolved" || s == "Rejected")
                .WithMessage("Status must be either 'Pending', 'Resolved', or 'Rejected'.");

            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User selection is required.")
                .GreaterThan(0).WithMessage("Please select a valid user.");

            RuleFor(c => c.ComplaintTypeId)
                .NotEmpty().WithMessage("Complaint Type is required.")
                .GreaterThan(0).WithMessage("Please select a valid complaint type.");

            RuleFor(c => c.FacilityTypeId)
                .NotEmpty().WithMessage("Facility Type is required.")
                .GreaterThan(0).WithMessage("Please select a valid facility type.");
        }
    }
}
