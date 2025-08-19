namespace Grievance_Management_System.Models
{
    public class DashboardStats
    {
        public int TotalComplaints { get; set; }
        public int ResolvedComplaints { get; set; }
        public int PendingComplaints { get; set; }
        public int TotalCitizens { get; set; }
        public int TotalFacilities { get; set; }
        public List<RecentComplaint> RecentComplaints { get; set; }
        public List<string> ComplaintTypes { get; set; }
        public List<int> ComplaintTypeCounts { get; set; }
        public List<object> CalendarEvents { get; set; }
        public List<Message> Messages { get; set; }
        public List<ToDoItem> ToDoList { get; set; }
    }

    public class RecentComplaint
    {
        public int Id { get; set; }
        public string CitizenName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }

    public class Message
    {
        public string SenderName { get; set; }
        public string TimeAgo { get; set; }
        public string MessageText { get; set; }
    }

    public class ToDoItem
    {
        public string TaskText { get; set; }
        public bool IsCompleted { get; set; }
    }

}
