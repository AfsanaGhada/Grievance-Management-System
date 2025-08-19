using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Grievance_Management_System.Models;
using Grievance_Management_System.Data;
using FluentValidation;


namespace Grievance_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintTypeController : ControllerBase
    {
        private readonly GrievanceManagementSystemContext _context;
        private readonly IValidator<ComplaintType> _validator;


        public ComplaintTypeController(GrievanceManagementSystemContext context, IValidator<ComplaintType> validator)
        {
            _context = context;
            _validator = validator;
        }

        // GET: api/ComplaintType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplaintType>>> GetComplaintTypes()
        {
            return await _context.ComplaintTypes.ToListAsync();
        }

        // GET: api/ComplaintType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintType>> GetComplaintType(int id)
        {
            var complaintType = await _context.ComplaintTypes.FindAsync(id);

            if (complaintType == null)
            {
                return NotFound();
            }

            return complaintType;
        }

        // PUT: api/ComplaintType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplaintType(int id, ComplaintType complaintType)
        {
            if (id != complaintType.ComplaintTypeId)
            {
                return BadRequest();
            }

            _context.Entry(complaintType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintTypeExists(id))
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

        // POST: api/ComplaintType
        [HttpPost]
        public async Task<ActionResult<ComplaintType>> PostComplaintType(ComplaintType complaintType)
        {
            var validationResult = await _validator.ValidateAsync(complaintType);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }
            _context.ComplaintTypes.Add(complaintType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComplaintType", new { id = complaintType.ComplaintTypeId }, complaintType);
        }

        // DELETE: api/ComplaintType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaintType(int id)
        {
            var complaintType = await _context.ComplaintTypes.FindAsync(id);
            if (complaintType == null)
            {
                return NotFound();
            }

            _context.ComplaintTypes.Remove(complaintType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComplaintTypeExists(int id)
        {
            return _context.ComplaintTypes.Any(e => e.ComplaintTypeId == id);
        }
    }
}
