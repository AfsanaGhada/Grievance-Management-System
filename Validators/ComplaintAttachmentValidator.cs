using FluentValidation;
using Grievance_Management_System.Models;

namespace Grievance_Management_System.Validators
{
    public class ComplaintAttachmentValidator : AbstractValidator<ComplaintAttachment>
    {
        public ComplaintAttachmentValidator()
        {
            RuleFor(a => a.ComplaintId)
                .NotEmpty().WithMessage("Complaint selection is required.")
                .GreaterThan(0).WithMessage("Please select a valid complaint.");

            RuleFor(a => a.FilePath)
                .NotEmpty().WithMessage("File path is required.")
                .MaximumLength(500).WithMessage("File path must not exceed 500 characters.");

            RuleFor(a => a.UploadedAt)
                .NotEmpty().WithMessage("Upload date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Upload date cannot be in the future.");
        }
    }
}


