using FluentValidation;
using Grievance_Management_System.Models;

namespace Grievance_Management_System.Validators
{
    public class FeedbackValidator : AbstractValidator<Feedback>
    {
        public FeedbackValidator()
        {
            RuleFor(f => f.ComplaintId)
                .NotEmpty().WithMessage("Complaint ID is required.")
                .GreaterThan(0).WithMessage("Complaint ID must be a valid value.");

            RuleFor(f => f.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .GreaterThan(0).WithMessage("User ID must be a valid value.");

            RuleFor(f => f.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.")
                .When(f => f.Rating.HasValue);

            RuleFor(f => f.Comments)
                .MaximumLength(500).WithMessage("Comments must not exceed 500 characters.")
                .When(f => !string.IsNullOrEmpty(f.Comments));
        }
    }
}
