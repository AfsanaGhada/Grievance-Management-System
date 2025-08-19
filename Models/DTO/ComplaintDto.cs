public class ComplaintDto
{
    public int ComplaintId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string FacilityName { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
}
