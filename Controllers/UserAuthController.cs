using Grievance_Management_System.Data;
using Grievance_Management_System.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Grievance_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly GrievanceManagementSystemContext _context;
        private readonly IConfiguration _configuration;


        public UserAuthController(GrievanceManagementSystemContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // 🔑 Generate Token with Role & Expiry from appsettings.json
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var expiryMinutes = Convert.ToDouble(jwtSettings["TokenExpiryMinutes"]);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Grievance_Management_System.Models.LoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid Email or Password." });
            }
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    user.UserId,
                    user.FullName,
                    user.Email,
                    user.Role,
                    user.Phone,
                    user.CityId,
                    user.CreatedDate
                }
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Models.RegisterRequest model)

        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                return Conflict(new { message = "Email already registered." });
            }

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                Role = string.IsNullOrEmpty(model.Role) ? "User" : model.Role,
                Phone = model.Phone,
                CityId = model.CityId,
                CreatedDate = DateTime.Now
            };
            _context.Users.Add(user);
             await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful." });
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _context.Cities
                .Select(c => new
                {
                    c.CityId,
                    c.CityName
                })
                .ToListAsync();

            return Ok(cities);
        }


    }
}
