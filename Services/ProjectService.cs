using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EmployeeManagement.Models.DTO;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using EmployeeManagement.Data;

namespace EmployeeManagement.Services
{
    public class ProjectService: IProjectService
    {
    private readonly EmployeeManagementDbContext dbContext;
    private readonly IMapper mapper;

public ProjectService(EmployeeManagementDbContext dbContext, IMapper mapper)
    {
              this.dbContext = dbContext;
        this.mapper=mapper;
    }
public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects= await  dbContext.Projects.ToListAsync();
            return mapper.Map<IEnumerable<ProjectDto>>(projects);

        }

public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            var project= await dbContext.Projects.FindAsync(id);
            return project==null ? null : mapper.Map<ProjectDto>(project);
        }

public async Task<ProjectDto> CreateAsync(AddProjectRequestDto addProjectRequest)
        {
               var employeeExists = await dbContext.Employees
            .AnyAsync(e => e.Id==addProjectRequest.EmployeeId);

        if (!employeeExists)
            throw new Exception("Employee not found");

        // 2. Map DTO → Entity
        var project = mapper.Map<Project>(addProjectRequest);

        // 3. Save
        await dbContext.Projects.AddAsync(project);
        await dbContext.SaveChangesAsync();

        // 4. Load Employee for response mapping
        await dbContext.Entry(project)
            .Reference(p => p.Employee)
            .LoadAsync();

        // 5. Entity → DTO
        return mapper.Map<ProjectDto>(project);

        
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
             throw new NotImplementedException();
        }
    }
}