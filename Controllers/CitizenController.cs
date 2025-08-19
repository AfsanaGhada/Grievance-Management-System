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
    public class CitizenController : ControllerBase
    {
        private readonly GrievanceManagementSystemContext _context;
        private readonly IValidator<Citizen> _validator;

        public CitizenController(GrievanceManagementSystemContext context, IValidator<Citizen> validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetCitizens()
        {
            var citizens = await _context.Citizens
                .Include(c => c.City)
                .Include(c => c.User)
                .Select(c => new
                {
                    c.CitizenId,
                    c.FullName,
                    c.Phone,
                    c.Address,
                    c.Gender,
                    c.Dob,
                    c.CityId,
                    CityName = c.City != null ? c.City.CityName : null,
                    c.UserId,
                    UserName = c.User != null ? c.User.FullName : null,
                    c.CreatedDate,
                    c.ModifiedDate
                })
                .ToListAsync();

            return Ok(citizens);
        }


        // GET: api/Citizen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Citizen>> GetCitizen(int id)
        {
            var citizen = await _context.Citizens.FindAsync(id);

            if (citizen == null)
            {
                return NotFound();
            }

            return citizen;
        }

        // PUT: api/Citizen/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitizen(int id, Citizen citizen)
        {
            if (id != citizen.CitizenId)
            {
                return BadRequest();
            }

            var existingCitizen = await _context.Citizens.FindAsync(id);
            if (existingCitizen == null)
            {
                return NotFound();
            }

            //  Update only intended editable fields
            existingCitizen.FullName = citizen.FullName;
            existingCitizen.Phone = citizen.Phone;
            existingCitizen.Address = citizen.Address;
            existingCitizen.Gender = citizen.Gender;
            existingCitizen.CityId = citizen.CityId;
            existingCitizen.ModifiedDate = DateTime.Now; // Track modification

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenExists(id))
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


        // POST: api/Citizen
        [HttpPost]
        public async Task<ActionResult<Citizen>> PostCitizen(Citizen citizen)
        {
            var validationResult = await _validator.ValidateAsync(citizen);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }
            _context.Citizens.Add(citizen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitizen", new { id = citizen.CitizenId }, citizen);
        }

        // DELETE: api/Citizen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitizen(int id)
        {
            var citizen = await _context.Citizens.FindAsync(id);
            if (citizen == null)
            {
                return NotFound();
            }

            _context.Citizens.Remove(citizen);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetCitizensWithCity()
        {
            var data = await _context.Citizens
                .Include(c => c.City)
                .Select(c => new
                {
                    citizenId = c.CitizenId,
                    fullName = c.FullName + " - " + c.City.CityName
                })
                .ToListAsync();

            return Ok(data);
        }

        private bool CitizenExists(int id)
        {
            return _context.Citizens.Any(e => e.CitizenId == id);
        }
    }
}
