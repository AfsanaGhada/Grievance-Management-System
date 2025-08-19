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
    public class FacilityTypeController : ControllerBase
    {
        private readonly GrievanceManagementSystemContext _context;
        private readonly IValidator<FacilityType> _validator;



        public FacilityTypeController(GrievanceManagementSystemContext context, IValidator<FacilityType> validator)
        {
            _context = context;
            _validator = validator;
        }

        // GET: api/FacilityType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacilityType>>> GetFacilityTypes()
        {
            return await _context.FacilityTypes.ToListAsync();
        }

        // GET: api/FacilityType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacilityType>> GetFacilityType(int id)
        {
            var facilityType = await _context.FacilityTypes.FindAsync(id);

            if (facilityType == null)
            {
                return NotFound();
            }

            return facilityType;
        }

        // PUT: api/FacilityType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacilityType(int id, FacilityType facilityType)
        {
            if (id != facilityType.FacilityTypeId)
            {
                return BadRequest();
            }

            _context.Entry(facilityType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacilityTypeExists(id))
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

        // POST: api/FacilityType
        [HttpPost]
        public async Task<ActionResult<FacilityType>> PostFacilityType(FacilityType facilityType)
        {
            var validationResult = await _validator.ValidateAsync(facilityType);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }
            _context.FacilityTypes.Add(facilityType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacilityType", new { id = facilityType.FacilityTypeId }, facilityType);
        }

        // DELETE: api/FacilityType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacilityType(int id)
        {
            var facilityType = await _context.FacilityTypes.FindAsync(id);
            if (facilityType == null)
            {
                return NotFound();
            }

            _context.FacilityTypes.Remove(facilityType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacilityTypeExists(int id)
        {
            return _context.FacilityTypes.Any(e => e.FacilityTypeId == id);
        }
    }
}
