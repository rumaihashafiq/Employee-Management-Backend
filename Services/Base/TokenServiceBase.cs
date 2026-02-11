using EmployeeManagement.Models.Domain;
namespace EmployeeManagement.Services.Base
{
    

public abstract class TokenServiceBase
    {
        public abstract string CreateToken(Employee employee);
    }



}