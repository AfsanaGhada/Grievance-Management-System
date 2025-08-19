using System.ComponentModel.DataAnnotations;

namespace Grievance_Management_System.Models
{
    public class RegisterRequest
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public string? Role { get; set; }

        public string? Phone { get; set; }

        public int? CityId { get; set; }
    }
}
