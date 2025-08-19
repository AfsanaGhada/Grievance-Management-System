using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grievance_Management_System.Models;

public partial class FacilityType
{
    public int FacilityTypeId { get; set; }

    public string FacilityName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    [NotMapped] 
    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
    [JsonIgnore]
    [NotMapped]
    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();
}
