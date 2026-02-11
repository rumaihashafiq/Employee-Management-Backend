using EmployeeManagement.Models.DTO;

namespace EmployeeManagement.Services.Interfaces
{
    public interface IProjectService
    {

        Task<IEnumerable<ProjectDto>> GetAllAsync();
    Task<ProjectDto> CreateAsync(AddProjectRequestDto addProjectRequest);
    
    }
}