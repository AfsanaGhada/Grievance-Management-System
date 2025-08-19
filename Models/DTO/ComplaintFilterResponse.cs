public class ComplaintFilterResponse
{
    public string Username { get; set; } = null!;
    public int TotalComplaints { get; set; }
    public List<ComplaintDto> Complaints { get; set; } = new();
    public List<string> FacilitiesAccessed { get; set; } = new();
}
