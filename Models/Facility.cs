using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grievance_Management_System.Models;

public partial class Facility
{
    public int FacilityId { get; set; }

    public string FacilityName { get; set; } = null!;

    public int FacilityTypeId { get; set; }

    public string? Location { get; set; }

    public int? CityId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual City? City { get; set; }
    [JsonIgnore]
    [NotMapped]
    public virtual FacilityType? FacilityType { get; set; } = null!;
}
