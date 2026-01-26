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
    }
}