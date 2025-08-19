using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grievance_Management_System.Models
{
    public partial class Complaint
    {
        public int ComplaintId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime ComplaintDate { get; set; }

        public string Status { get; set; } = null!;

        public int UserId { get; set; }

        public int ComplaintTypeId { get; set; }

        public int FacilityTypeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<ComplaintAttachment>? ComplaintAttachments { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<ComplaintStatusHistory>? ComplaintStatusHistories { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ComplaintType? ComplaintType { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual FacilityType? FacilityType { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Feedback>? Feedbacks { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual User? User { get; set; }

        public int? FacilityId { get; set; }
        public Facility Facility { get; set; } = null!;


    }
}