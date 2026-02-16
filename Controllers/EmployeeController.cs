using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Data;
using System.Security.Claims;
using EmployeeManagement.Models.Domain;
namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly EmployeeManagementDbContext dbContext;

        public EmployeeController(IEmployeeService employeeService, EmployeeManagementDbContext context)
        {
            this.employeeService = employeeService;
            this.dbContext = context;
        }

[HttpGet("MyProfile")]
[Authorize]
public async Task<ActionResult<Employee>> GetMyProfile()
{
    var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

    if (!Guid.TryParse(userIdString, out Guid userId))
    {
        return BadRequest("Invalid user ID");
    }

    var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == userId);

    if (employee == null) return NotFound();

    return Ok(employee);
}
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var employee = await employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddEmployeeRequestDto dto)
        {
            var employee = await employeeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateRequestDto dto)
        {
            var employee = await employeeService.UpdateAsync(id, dto);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await employeeService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }

        }

        [HttpGet("dashboard")]
        public async Task<object> GetDashboardDataAsync(Guid userId, bool isAdmin)
        {
            if (isAdmin)
                return await dbContext.Employees.ToListAsync();

#pragma warning disable CS8603 // Possible null reference return.
            return await dbContext.Employees
        .Where(e => e.Id == userId)
        .Select(e => new
        {
            e.Id,
            e.Name,
            e.Email,
            e.Department
        })
        .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

    }
}
