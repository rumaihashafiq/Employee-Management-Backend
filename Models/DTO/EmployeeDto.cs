using System.ComponentModel.DataAnnotations;
namespace EmployeeManagement.Models.DTO
{
    public class EmployeeDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }

    public required string Department { get; set; }
    public DateTime DateOfJoining { get; set; }=DateTime.Now;
}
}
