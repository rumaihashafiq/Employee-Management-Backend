using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models.DTO;
using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Components.Routing;

namespace EmployeeManagement.Controllers
{

[ApiController]
[Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;


        public ProjectController(IProjectService projectService)
        {
         this.projectService=projectService;   
        }
    [HttpGet]
    public async Task<IActionResult> GetAll()
        {
            var projects= await projectService.GetAllAsync();
            return Ok(projects);
        }
[HttpPost]
public async Task<IActionResult> CreateProject(AddProjectRequestDto request)
{
    var project = await projectService.CreateAsync(request);
    return Ok(project);
}
    }
}