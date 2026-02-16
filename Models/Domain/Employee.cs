using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models.Domain
{

    [Index(nameof(Email), IsUnique = true)]
    public class Employee
    {

        public Guid Id { get; set; }
        public string Role { get; set; } = "Employee";

        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Department { get; set; }

        public DateTime DateOfJoining { get; set; } = DateTime.UtcNow;

       public bool  IsActive { get; set; }=false;

         public ICollection<Project> Projects { get; set; } = new List<Project>();
    }

}
