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
    public class CityController : ControllerBase
    {
        private readonly GrievanceManagementSystemContext _context;
        private readonly IValidator<City> _validator;


        public CityController(GrievanceManagementSystemContext context, IValidator<City> validator)
        {
            _context = context;
            _validator = validator;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cities = await _context.Cities
                .Include(c => c.State)
                    .ThenInclude(s => s.Country)
                .Select(c => new City
                {
                    CityId = c.CityId,
                    CityName = c.CityName,
                    CityCode = c.CityCode,
                    StateId = c.StateId,
                    StateName = c.State != null ? c.State.StateName : null,
                    CountryId = c.State != null ? c.State.CountryId : null,
                    CountryName = c.State != null && c.State.Country != null ? c.State.Country.CountryName : null
                })
                .ToListAsync();

            return Ok(cities);
        }


        // GET: api/City/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }
        [HttpGet("GetCityState")]
        public async Task<ActionResult> GetCityState(string? cityName, int? stateId, int? countryId)
        {
            if (string.IsNullOrEmpty(cityName) || stateId == null || countryId == null)
            {
                return BadRequest("CityName, StateId, and CountryId are required.");
            }

            var city = await _context.Cities
                .Include(c => c.State)
                .ThenInclude(s => s.Country)
                .FirstOrDefaultAsync(c => c.CityName == cityName && c.StateId == stateId && c.State.CountryId == countryId);

            if (city == null)
            {
                return NotFound("No city found with the provided details.");
            }

            return Ok(city);
        }


        // PUT: api/City/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.CityId)
            {
                return BadRequest();
            }

            //  Clear navigation properties to avoid circular reference errors
            city.Citizens = null;
            city.Users = null;
            city.Facilities = null;
            city.State = null;
            city.Country = null;

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/City
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            var validationResult = await _validator.ValidateAsync(city);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.CityId }, city);
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            // Check for related facilities
            bool hasFacilities = await _context.Facilities.AnyAsync(f => f.CityId == id);
            if (hasFacilities)
            {
                return BadRequest("Cannot delete this city because there are facilities associated with it.");
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("by-state/{stateId}")]
        public async Task<IActionResult> GetCitiesByState(int stateId)
        {
            var cities = await _context.Cities
                .Where(c => c.StateId == stateId)
                .Select(c => new
                {
                    c.CityId,
                    c.CityName
                })
                .ToListAsync();

            return Ok(cities);
        }
        [HttpGet("Top10")]
        public async Task<ActionResult<IEnumerable<City>>> GetTop10Cities()
        {
            var Cities = await _context.Cities
                .OrderByDescending(c => c.CreatedDate)
                .Take(10)
                .ToListAsync();

            return Ok(Cities);
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.CityId == id);
        }
    }
}
