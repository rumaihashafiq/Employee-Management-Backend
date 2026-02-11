using AutoMapper;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.DTO;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // 1. Incoming: Request DTO → Domain
        // If the user sends a string and the Domain is a DateTime, 
        // AutoMapper handles basic strings automatically.
        CreateMap<AddProjectRequestDto, Project>();

        // 2. Outgoing: Domain → Response DTO
        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.StartDate, 
                opt => opt.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.EndDate, 
                opt => opt.MapFrom(src => src.EndDate.HasValue 
                    ? src.EndDate.Value.ToString("yyyy-MM-dd") 
                    : null))
            .ForMember(dest => dest.DurationInDays,
                opt => opt.MapFrom(src =>
                    src.EndDate.HasValue
                        ? (src.EndDate.Value - src.StartDate).TotalDays
                        : (DateTime.UtcNow - src.StartDate).TotalDays));
    }
}