using Grievance_Management_System.Data;
using Grievance_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grievance_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly GrievanceManagementSystemContext _context;

        public DashboardController(GrievanceManagementSystemContext context)
        {
            _context = context;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var stats = new DashboardStats
            {
                TotalComplaints = await _context.Complaints.CountAsync(),
                ResolvedComplaints = await _context.Complaints.CountAsync(c => c.Status == "Resolved"),
                PendingComplaints = await _context.Complaints.CountAsync(c => c.Status == "Pending"),
                TotalCitizens = await _context.Citizens.CountAsync(),
                TotalFacilities = await _context.Facilities.CountAsync()
            };

            // Fetching recent complaints with Citizen Name using UserId join
            var recentComplaints = await (
                from complaint in _context.Complaints
                join citizen in _context.Citizens on complaint.UserId equals citizen.UserId
                orderby complaint.CreatedDate descending
                select new RecentComplaint
                {
                    Id = complaint.ComplaintId,
                    CitizenName = citizen.FullName,
                    Date = complaint.CreatedDate,
                    Status = complaint.Status
                })
                .Take(5)
                .ToListAsync();

            stats.RecentComplaints = recentComplaints;

            // Complaint Types with counts using ComplaintTypeId (no navigation)
            var complaintTypeGroups = await _context.Complaints
                .GroupBy(c => c.ComplaintTypeId)
                .Select(g => new
                {
                    Type = g.Key.ToString(), // using ID as string
                    Count = g.Count()
                })
                .ToListAsync();

            stats.ComplaintTypes = complaintTypeGroups.Select(x => x.Type).ToList();
            stats.ComplaintTypeCounts = complaintTypeGroups.Select(x => x.Count).ToList();

            // Add dummy messages
            stats.Messages = new List<Message>
            {
                new Message { SenderName = "Admin", TimeAgo = "2h ago", MessageText = "Welcome to the dashboard." },
                new Message { SenderName = "Support", TimeAgo = "1d ago", MessageText = "Weekly backup completed." }
            };

            // Add dummy calendar events
            stats.CalendarEvents = new List<object>
            {
                new { title = "System Backup", start = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") },
                new { title = "Monthly Meeting", start = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd") }
            };

            // Add dummy todo list
            stats.ToDoList = new List<ToDoItem>
            {
                new ToDoItem { TaskText = "Review pending complaints", IsCompleted = false },
                new ToDoItem { TaskText = "Prepare weekly report", IsCompleted = true }
            };

            return Ok(stats);
        }
    }
}
