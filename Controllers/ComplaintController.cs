using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Grievance_Management_System.Models;
using Grievance_Management_System.Data;

namespace Grievance_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly GrievanceManagementSystemContext _context;

        public ComplaintController(GrievanceManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/Complaint
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaints()
        {
            return await _context.Complaints.ToListAsync();
        }

        // GET: api/Complaint/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Complaint>> GetComplaint(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);

            if (complaint == null)
            {
                return NotFound();
            }

            return complaint;
        }

        // PUT: api/Complaint/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplaint(int id, Complaint complaint)
        {
            if (id != complaint.ComplaintId)
            {
                return BadRequest();
            }

            _context.Entry(complaint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Complaint
        [HttpPost]
        public async Task<ActionResult<Complaint>> PostComplaint(Complaint complaint)
        {
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComplaint", new { id = complaint.ComplaintId }, complaint);
        }

        // DELETE: api/Complaint/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaint(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }

            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("api/complaints/search")]
        public async Task<IActionResult> SearchComplaints([FromQuery] string status, [FromQuery] int? cityId)
        {
            var query = _context.Complaints.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(c => c.Status == status);

            if (cityId.HasValue)
                query = query.Where(c => c.User.CityId == cityId);

            var results = await query
                .Select(c => new {
                    c.ComplaintId,
                    c.Title,
                    c.Status,
                    c.ComplaintDate,
                    UserFullName = c.User.FullName
                })
                .ToListAsync();

            return Ok(results);
        }
        [HttpGet("Top10")]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetTop10Complaints()
        {
            var complaints = await _context.Complaints
                .OrderByDescending(c => c.CreatedDate)
                .Take(10)
                .ToListAsync();

            return Ok(complaints);
        }


        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.ComplaintId == id);
        }
    }
}
