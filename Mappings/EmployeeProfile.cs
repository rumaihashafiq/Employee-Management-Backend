using AutoMapper;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTO;

namespace EmployeeManagement.Mappings
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            //to display data to user/employee
            CreateMap<Employee, EmployeeDto>();

            //to add data to database
        CreateMap<AddEmployeeRequestDto, Employee>();

        //to update data in database
        CreateMap<UpdateRequestDto, Employee>();
        }
    }
}