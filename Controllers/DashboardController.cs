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
            //var stats = await _context.

            return Ok(stats);
        }
    }
}
