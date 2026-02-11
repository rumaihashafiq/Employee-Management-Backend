using EmployeeManagement.Models.Domain;

namespace EmployeeManagement.Services.Interfaces
{
    public interface IAuthService
    {
         string CreateToken(Employee employee);
    }
}