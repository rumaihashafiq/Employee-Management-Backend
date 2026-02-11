using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Domain
{
    public class EmployeeTask
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;

        // Foreign Key to Employee
        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}