using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grievance_Management_System.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public int UserId { get; set; }
    [NotMapped]
    public string? FullName { get; set; }
    [NotMapped]
    public string? Phone { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    [NotMapped] 
    public virtual User? User { get; set; } 
}
