

using Grievance_Management_System.Models;
using Microsoft.EntityFrameworkCore;


namespace Grievance_Management_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<Facility> Facilities { get; set; }
    }
}
