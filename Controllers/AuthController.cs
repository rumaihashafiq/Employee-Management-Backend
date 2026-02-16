using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTO;
using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EmployeeManagementDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(EmployeeManagementDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        [HttpPost("signup")]
        public IActionResult Signup([FromBody] AddEmployeeRequestDto registerDto)
        {
            // 1. UNIQUE EMAIL VALIDATION
            // Check if any employee already exists with this email
            var existingEmployee = _context.Employees
                .FirstOrDefault(x => x.Email.ToLower() == registerDto.Email.ToLower());

            if (existingEmployee != null)
            {
                // Return a 400 Bad Request if email is taken
                return BadRequest(new { message = "Email is already in use. Please use a different email." });
            }

            // 2. SAVE TO DATABASE
            // Create the entity from your DTO
            var newEmployee = new Employee
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password, // Ideally hash this later
                Department = registerDto.Department,
                DateOfJoining = DateTime.Now // Automatic
            };

            try
            {
                _context.Employees.Add(newEmployee);
                _context.SaveChanges(); // This stores it in SQL!

                return Ok(new { message = "Registration successful!" });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while saving to the database.");
            }
        }
        // [HttpPost("login")]
        // public IActionResult Login([FromBody] LoginDto loginDto) {
        //     var employee = _context.Employees
        //         .FirstOrDefault(x => x.Email == loginDto.Email && x.Password == loginDto.Password);

        //     if (employee == null) return Unauthorized("Invalid credentials.");

        //     var token = _authService.CreateToken(employee);
        //     return Ok(new { Token = token });
        // }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.Email == loginDto.Email && x.Password == loginDto.Password);

            if (employee == null) return Unauthorized("Invalid credentials.");
              employee.IsActive = true;
            await _context.SaveChangesAsync();

            var token = _authService.CreateToken(employee);
            return Ok(new
            {
                Token = token,
                isActive = employee.IsActive
            });
        }

    }

    public record LoginDto(string Email, string Password);


}