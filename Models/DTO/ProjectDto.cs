namespace EmployeeManagement.Models.DTO
{
    public class ProjectDto
    {
        public Guid ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? Description { get; set; }
        public string? StartDate { get; set; } // Changed to string to hold formatted date
        public string? EndDate { get; set; }   // Changed to string
        public double? DurationInDays { get; set; }
        public bool IsCompleted { get; set; }
    }
}