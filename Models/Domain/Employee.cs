namespace EmployeeManagement.Models.Domain
{
    public class Employee
{
    public Guid Id{ get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }

    public required string Department { get; set; }

    public DateTime DateOfJoining { get; set; }=DateTime.Now;
}

}
