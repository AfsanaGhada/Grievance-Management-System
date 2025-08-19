using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grievance_Management_System.Models;

public partial class State
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public string? StateCode { get; set; }

    public int CountryId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
    [JsonIgnore]

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    [JsonIgnore]
    public virtual Country? Country { get; set; }


    [JsonIgnore]
    [NotMapped]
    public string? CountryName { get; set; } 

}
