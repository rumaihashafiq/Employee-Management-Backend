using EmployeeManagement.Models.DTO;
namespace EmployeeManagement.Services.Interfaces
{
    public interface IEmployeeService
{
      Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto?> GetByIdAsync(Guid id);
    Task<EmployeeDto> CreateAsync(AddEmployeeRequestDto dto);
    Task<EmployeeDto?> UpdateAsync(Guid id, UpdateRequestDto dto);
    Task<bool> DeleteAsync(Guid id); 
    //Task<IEnumerable<object>> GetDashboardDataAsync(Guid? userId, bool isAdmin);   
   
 Task<object> GetDashboardDataAsync(Guid userId, bool isAdmin);

}





}
