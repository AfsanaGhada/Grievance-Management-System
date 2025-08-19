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
    public class ComplaintAttachmentController : ControllerBase
    {
        private readonly GrievanceManagementSystemContext _context;
        private readonly IValidator<ComplaintAttachment> _validator;


        public ComplaintAttachmentController(GrievanceManagementSystemContext context, IValidator<ComplaintAttachment> validator)
        {
            _context = context;
            _validator = validator;
        }

        // GET: api/ComplaintAttachment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplaintAttachment>>> GetComplaintAttachments()
        {
            return await _context.ComplaintAttachments.ToListAsync();
        }

        // GET: api/ComplaintAttachment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintAttachment>> GetComplaintAttachment(int id)
        {
            var complaintAttachment = await _context.ComplaintAttachments.FindAsync(id);

            if (complaintAttachment == null)
            {
                return NotFound();
            }

            return complaintAttachment;
        }

        // PUT: api/ComplaintAttachment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplaintAttachment(int id, ComplaintAttachment complaintAttachment)
        {
            if (id != complaintAttachment.AttachmentId)
            {
                return BadRequest();
            }

            _context.Entry(complaintAttachment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintAttachmentExists(id))
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

        // POST: api/ComplaintAttachment
        [HttpPost]
        public async Task<ActionResult<ComplaintAttachment>> PostComplaintAttachment(ComplaintAttachment complaintAttachment)
        {
            var validationResult = await _validator.ValidateAsync(complaintAttachment);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }
            _context.ComplaintAttachments.Add(complaintAttachment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComplaintAttachment", new { id = complaintAttachment.AttachmentId }, complaintAttachment);
        }

        // DELETE: api/ComplaintAttachment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaintAttachment(int id)
        {
            var complaintAttachment = await _context.ComplaintAttachments.FindAsync(id);
            if (complaintAttachment == null)
            {
                return NotFound();
            }

            _context.ComplaintAttachments.Remove(complaintAttachment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComplaintAttachmentExists(int id)
        {
            return _context.ComplaintAttachments.Any(e => e.AttachmentId == id);
        }
    }
}
