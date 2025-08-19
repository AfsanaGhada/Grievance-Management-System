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
    public class ComplaintStatusHistoriesController : ControllerBase
    {
        private readonly GrievanceManagementSystemContext _context;

        public ComplaintStatusHistoriesController(GrievanceManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/ComplaintStatusHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplaintStatusHistory>>> GetComplaintStatusHistories()
        {
            return await _context.ComplaintStatusHistories.ToListAsync();
        }

        // GET: api/ComplaintStatusHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintStatusHistory>> GetComplaintStatusHistory(int id)
        {
            var complaintStatusHistory = await _context.ComplaintStatusHistories.FindAsync(id);

            if (complaintStatusHistory == null)
            {
                return NotFound();
            }

            return complaintStatusHistory;
        }

        // PUT: api/ComplaintStatusHistories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplaintStatusHistory(int id, ComplaintStatusHistory complaintStatusHistory)
        {
            if (id != complaintStatusHistory.HistoryId)
            {
                return BadRequest();
            }

            _context.Entry(complaintStatusHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintStatusHistoryExists(id))
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

        // POST: api/ComplaintStatusHistories
        [HttpPost]
        public async Task<ActionResult<ComplaintStatusHistory>> PostComplaintStatusHistory(ComplaintStatusHistory complaintStatusHistory)
        {
            _context.ComplaintStatusHistories.Add(complaintStatusHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComplaintStatusHistory", new { id = complaintStatusHistory.HistoryId }, complaintStatusHistory);
        }

        // DELETE: api/ComplaintStatusHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaintStatusHistory(int id)
        {
            var complaintStatusHistory = await _context.ComplaintStatusHistories.FindAsync(id);
            if (complaintStatusHistory == null)
            {
                return NotFound();
            }

            _context.ComplaintStatusHistories.Remove(complaintStatusHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComplaintStatusHistoryExists(int id)
        {
            return _context.ComplaintStatusHistories.Any(e => e.HistoryId == id);
        }
    }
}
