using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grievance_Management_System.Models
{
    public partial class User
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string? Phone { get; set; }

        public int? CityId { get; set; }

        [NotMapped]
        public string? CityName { get; set; }

        [NotMapped]
        public string? StateName { get; set; }

        [NotMapped]
        public string? CountryName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Citizen> Citizens { get; set; } = new List<Citizen>();

        [JsonIgnore]
        [NotMapped]
        public virtual City? City { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
