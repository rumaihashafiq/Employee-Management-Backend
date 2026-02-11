using Microsoft.VisualBasic;

namespace EmployeeManagement.Models.DTO
{
    public class AddProjectRequestDto
    {
        public required string ProjectName { get; set; }
        public string? Description { get; set; }


        public DateTime StartDate { get; set; }=DateTime.Now;
      public DateTime? EndDate { get; set; }
        public Guid EmployeeId{ get; set; }
    }
}