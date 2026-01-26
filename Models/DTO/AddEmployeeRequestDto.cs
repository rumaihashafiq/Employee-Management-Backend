using System.ComponentModel.DataAnnotations;
namespace EmployeeManagement.Models.DTO
{
    
public class AddEmployeeRequestDto
{

    [Required (ErrorMessage ="Employee name is required")]
[StringLength(100, MinimumLength =7, ErrorMessage ="Invalid name")]

    public required string Name { get; set; }

    [Required(ErrorMessage ="Required field")]
    [EmailAddress(ErrorMessage ="Invalid format")]
    public required string Email {get; set;}


    [Required(ErrorMessage ="Department is required")]
    [StringLength(50, MinimumLength =2, ErrorMessage ="Invalid name")]
    public required string Department { get; set; }


    
}
}