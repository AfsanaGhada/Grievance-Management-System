using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grievance_Management_System.Models;

public partial class Citizen
{
    public int CitizenId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public int? CityId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual City? City { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual User? User { get; set; }

    [JsonIgnore]
    [NotMapped]
    public string? UserName { get; set; }

    [JsonIgnore]
    [NotMapped]
    public string? CityName { get; set; }
}
