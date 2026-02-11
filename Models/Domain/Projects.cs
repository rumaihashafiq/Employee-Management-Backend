namespace EmployeeManagement.Models.Domain
{
public class Project
{
    public Guid ProjectId { get; private set; }
    public required string ProjectName{get; set;}
    public string? Description { get; set; }

    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public TimeSpan? Duration =>
        EndDate.HasValue ? EndDate.Value - StartDate : null;

    public Guid EmployeeId { get; private set; }
    public Employee? Employee { get; set; }

    protected Project()
    {
        StartDate = DateTime.UtcNow;
    }

    public Project(Guid employeeId)
    {
        ProjectId = Guid.NewGuid();
        EmployeeId = employeeId;
        StartDate = DateTime.UtcNow;
    }

    public void Complete()
    {
        EndDate = DateTime.UtcNow;

        if (EndDate < StartDate)
            throw new InvalidOperationException("EndDate cannot be before StartDate");
    }
}
}