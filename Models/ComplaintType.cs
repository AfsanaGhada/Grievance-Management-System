using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grievance_Management_System.Models
{
    public partial class ComplaintType
    {
        public int ComplaintTypeId { get; set; }

        public string ComplaintName { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Complaint>? Complaints { get; set; } = new List<Complaint>();
    }
}
