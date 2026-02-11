using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Services;
using EmployeeManagement.Migrations;
using EmployeeManagement.Services.Interfaces;
namespace EmployeeManagement.Controllers
{
    public class DashboardController :ControllerBase
    {
        private readonly EmployeeManagementDbContext  _context;
        private readonly IEmployeeService employeeService;


        public DashboardController(EmployeeManagementDbContext context, IEmployeeService employeeServices)
        {
            _context = context;
            employeeService = employeeServices;

        
        }

        // [Authorize] // This ensures the user must be logged in
        // [HttpGet("dashboard-data")]
        // public async Task<IActionResult> GetDashboard()
        // {
        //     // 1. Get User ID and Role from Token (JWT)
        //     var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //     var isAdmin = User.IsInRole("Admin");

        //     if (string.IsNullOrEmpty(userId)) return Unauthorized();

        //     // 2. Query Logic
        //     var query = _context.Employees
        //         .Include(e => e.Projects)
        //         .AsQueryable();

        //     // If not admin, filter to ONLY this user
        //     if (!isAdmin)
        //     {
        //         if (Guid.TryParse(userId, out Guid employeeId))
        //         {
        //             query = query.Where(e => e.Id == employeeId);
        //         }
        //     }

        //     var result = await query.Select(e => new
        //     {
        //         e.Name,
        //         e.Email,
        //         e.Department,
        //         Projects = e.Projects.Select(p => new { p.ProjectName, p.Description, p.StartDate }),
        //         Tasks = _context.EmployeeTasks
        //             .Where(t => t.EmployeeId == e.Id && !t.IsCompleted)
        //             .Select(t => new { t.Id, t.Title }) // Keep it simple for the frontend
        //             .ToList()
        //     }).ToListAsync();

        //     return Ok(result);
        // }

[Authorize]
[HttpGet("dashboard")]
public async Task<IActionResult> GetDashboard()
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userId == null)
        return Unauthorized("No user id in token");

    var parsedId = Guid.Parse(userId);

    var isAdmin = User.IsInRole("Admin");

    var data = await employeeService.GetDashboardDataAsync(parsedId, isAdmin);

    return Ok(new 
    {
        UserId = parsedId,
        IsAdmin = isAdmin,
        Data = data
    });
}




    }
}