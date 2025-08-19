using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Grievance_Management_System.Models;

public partial class ComplaintAttachment
{
    [Key]
    public int AttachmentId { get; set; }

    public int ComplaintId { get; set; }

    public string FilePath { get; set; } = null!;

    public DateTime UploadedAt { get; set; }
    [JsonIgnore]
    [NotMapped]
    public virtual Complaint? Complaint { get; set; } = null!;
}
