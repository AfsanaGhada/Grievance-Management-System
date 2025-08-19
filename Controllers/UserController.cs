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
using Microsoft.AspNetCore.Authorization;

namespace Grievance_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Applied to the entire controller

    public class UserController : ControllerBase
    {
        private readonly GrievanceManagementSystemContext _context;
        private readonly IValidator<User> _validator;


        public UserController(GrievanceManagementSystemContext context, IValidator<User> validator)
        {
            _context = context;
            _validator = validator;
        }

        //  This will require any authenticated user (no role restriction)
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            return Ok(new { message = "Welcome Authenticated User!" });
        }

        //  Override with specific roles
        [Authorize(Roles = "Admin")]
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        //  Allow anonymous access (overrides controller-level authorize)
        [AllowAnonymous]
        [HttpGet("public-info")]
        public IActionResult PublicInfo()
        {
            return Ok(new { message = "This is public info, no login required." });
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.City)
                    .ThenInclude(c => c.State)
                        .ThenInclude(s => s.Country)
                .Select(u => new User
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    Email = u.Email,
                    Password = u.Password,
                    Role = u.Role,
                    Phone = u.Phone,
                    CityId = u.CityId,
                    CityName = u.City != null ? u.City.CityName : null,
                    StateName = u.City != null && u.City.State != null ? u.City.State.StateName : null,
                    CountryName = u.City != null && u.City.State != null && u.City.State.Country != null
                        ? u.City.State.Country.CountryName
                        : null,
                    CreatedDate = u.CreatedDate,
                    ModifiedDate = u.ModifiedDate
                })
                .ToListAsync();

            return Ok(users);
        }


        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
                return BadRequest();

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return NotFound();

            // Update only intended editable fields
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;
            existingUser.Phone = user.Phone;
            existingUser.CityId = user.CityId;

            existingUser.ModifiedDate = DateTime.Now; // Track modification

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var validationResult = await _validator.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
