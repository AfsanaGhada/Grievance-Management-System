using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;




namespace Grievance_Management_System.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public string? CityCode { get; set; }

    public int StateId { get; set; }

    [NotMapped]
    public string? StateName { get; set; }

    [NotMapped]
    public int? CountryId { get; set; }

    [NotMapped]
    public string? CountryName { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual State? State { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual Country? Country { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual ICollection<User>? Users { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual ICollection<Citizen>? Citizens { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual ICollection<Facility>? Facilities { get; set; }


}
