using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTO;
using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeManagementDbContext dbContext;
           private readonly IMapper mapper;
        public EmployeeService(EmployeeManagementDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper=mapper;
        
    }

 public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await dbContext.Employees.ToListAsync();
            return mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

    
 public async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            return employee == null ? null : mapper.Map<EmployeeDto>(employee);
        }
  

 public async Task<EmployeeDto> CreateAsync(AddEmployeeRequestDto dto)
        {
            var exists = await dbContext.Employees.AnyAsync(e => e.Email == dto.Email);
            if (exists)
                throw new Exception("The user with this email already exists");

            var employee = mapper.Map<Employee>(dto);
            employee.DateOfJoining = DateTime.UtcNow;

            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return mapper.Map<EmployeeDto>(employee);
        }


 public async Task<EmployeeDto?> UpdateAsync(Guid id, UpdateRequestDto dto)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null) return null;

            mapper.Map(dto, employee);
            await dbContext.SaveChangesAsync();

            return mapper.Map<EmployeeDto>(employee);
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
public async Task<IEnumerable<EmployeeDto>> GetDashboardDataAsync(Guid? userId, bool isAdmin)
{
    var query = dbContext.Employees
        .Include(e => e.Projects)
        .AsQueryable();

    // Employee → only own data
    if (!isAdmin && userId.HasValue)
    {
        query = query.Where(e => e.Id == userId.Value);
    }

    var result = await query.Select(e => new
    {
        e.Id,
        e.Name,
        e.Email,
        e.Department,
        Projects = e.Projects.Select(p => new
        {
            p.ProjectName,
            p.Description,
            p.StartDate
        }).ToList()
    }).ToListAsync();

    return (IEnumerable<EmployeeDto>)result;
}

        public Task<EmployeeDto> GetDashboardDataAsync(Guid userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

        Task<object> IEmployeeService.GetDashboardDataAsync(Guid userId, bool isAdmin)
        {
            throw new NotImplementedException();
        }

    }
}