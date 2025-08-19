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
    public class StateController : ControllerBase
    {
        private readonly GrievanceManagementSystemContext _context;
        private readonly IValidator<State> _validator;


        public StateController(GrievanceManagementSystemContext context, IValidator<State> validator)
        {
            _context = context;
            _validator = validator;
        }

        //// GET: api/State
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetStates()
        {
            var states = await _context.States
                .Include(s => s.Country)  // Make sure Country is loaded
                .Select(s => new State
                {
                    StateId = s.StateId,
                    StateName = s.StateName,
                    StateCode = s.StateCode,
                    CountryId = s.CountryId,
                    CountryName = s.Country != null ? s.Country.CountryName : null
                })
                .ToListAsync();

            return Ok(states);
        }

        // GET: api/State/5
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(int id)
        {
            var state = await _context.States.FindAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            return state;
        }

       

        [HttpPost]
        public async Task<ActionResult<State>> PostState([FromBody] State state)
        {
            var validationResult = await _validator.ValidateAsync(state);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }
            state.CreatedDate = DateTime.Now;
            state.ModifiedDate = DateTime.Now;

            _context.States.Add(state);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetState", new { id = state.StateId }, state);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutState(int id, [FromBody] State state)
        {
            if (id != state.StateId)
                return BadRequest();

            state.ModifiedDate = DateTime.Now;
            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.States.Any(e => e.StateId == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/State/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // GET: api/State/by-country/5
        [HttpGet("by-country/{countryId}")]
        public async Task<IActionResult> GetStatesByCountry(int countryId)
        {
            var states = await _context.States
                .Where(s => s.CountryId == countryId)
                .Select(s => new { s.StateId, s.StateName })
                .ToListAsync();

            return Ok(states);
        }

        private bool StateExists(int id)
        {
            return _context.States.Any(e => e.StateId == id);
        }
    }
}
