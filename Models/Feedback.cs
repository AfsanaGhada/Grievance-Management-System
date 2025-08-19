using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grievance_Management_System.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }

        public int ComplaintId { get; set; }

        public int UserId { get; set; }

        public int? Rating { get; set; }

        public string? Comments { get; set; }

        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual Complaint? Complaint { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual User? User { get; set; }
    }
}
