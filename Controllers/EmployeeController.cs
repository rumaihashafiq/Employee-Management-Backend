using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Services.Interfaces;
namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var employee = await employeeService.GetByIdAsync(id);
            if (employee == null)
            {  
                return NotFound();    
            } 

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AddEmployeeRequestDto dto)
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
        public async Task<IActionResult> Delete([FromRoute]Guid id)
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
    }
}
