 using Microsoft.EntityFrameworkCore;
 using EmployeeManagement.Models.Domain;
namespace EmployeeManagement.Data
{
    public class EmployeeManagementDbContext : DbContext
    {
       public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext>dbContextOptions ): base(dbContextOptions)
        {
            
        }
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects{get; set;}
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
  modelBuilder.Entity<Project>()
    .HasOne(p => p.Employee)
    .WithMany(e => e.Projects)
    .HasForeignKey(p => p.EmployeeId);
}

    }
}