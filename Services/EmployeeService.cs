using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTO;
using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeManagementDbContext dbContext;

    public EmployeeService(EmployeeManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await dbContext.Employees.ToListAsync();

        return employees.Select(e => new EmployeeDto
        {
            Id = e.Id,
            Name = e.Name ?? string.Empty,
            Email = e.Email ?? string.Empty,
            Department = e.Department ?? string.Empty,
            DateOfJoining = e.DateOfJoining
        });
    }

    public async Task<EmployeeDto?> GetByIdAsync(Guid id)
    {
        var employee = await dbContext.Employees.FindAsync(id);
        if (employee == null)

            {
                return null;
 
            }

            else
            {
               return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name ?? string.Empty,
            Email = employee.Email ?? string.Empty,
            Department = employee.Department ?? string.Empty,
            DateOfJoining = employee.DateOfJoining
        }; 
            }
        
    }

    public async Task<EmployeeDto> CreateAsync(AddEmployeeRequestDto dto)
    {     
        //dto to domain model
        var employee = new Employee
        {
            Name = dto.Name,
            Email = dto.Email,
            Department = dto.Department,
            DateOfJoining = DateTime.UtcNow
        };
           var employeeEmail= await dbContext.Employees.AnyAsync(e=>e.Email==dto.Email);
            if (employeeEmail)
            {
                throw new Exception("The user with this email already exists");
            }

            else
            {
                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();
            return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name ?? string.Empty,
            Email = employee.Email ?? string.Empty,
            Department = employee.Department ?? string.Empty,
            DateOfJoining = employee.DateOfJoining
        };
            }
        

    
    }

    public async Task<EmployeeDto?> UpdateAsync(Guid id, UpdateRequestDto dto)
    {
        var employee = await dbContext.Employees.FindAsync(id);
        if (employee == null)
            {
                  return null;
            }
        
        employee.Name = dto.Name;
        employee.Department = dto.Department;

        await dbContext.SaveChangesAsync();

        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name ?? string.Empty,
            Email = employee.Email ?? string.Empty,
            Department = employee.Department ?? string.Empty,
            DateOfJoining = employee.DateOfJoining
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var employee = await dbContext.Employees.FindAsync(id);
        if (employee == null)

            {
                 return false;

            }
            else
            {
                     dbContext.Employees.Remove(employee);
        await dbContext.SaveChangesAsync();
        return true;
            }
   
    }
    }
}