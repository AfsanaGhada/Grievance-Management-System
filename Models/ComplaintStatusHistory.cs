using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grievance_Management_System.Models
{
    public partial class ComplaintStatusHistory
    {
        [Key]
        public int HistoryId { get; set; }

        public int ComplaintId { get; set; }

        public string Status { get; set; } = null!;

        public DateTime UpdatedAt { get; set; }

        public string? Remarks { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual Complaint? Complaint { get; set; }
    }
}
