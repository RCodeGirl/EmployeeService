using AutoMapper;
using EmployeesService.Api.Dtos;
using EmployeesService.Models;
namespace EmployeesService.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.Passport, opt => opt.MapFrom(src => src.Passport))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));
            CreateMap<Passport, PassportDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<PassportDto, Passport>();
            CreateMap<DepartmentDto, Department>();
            
        }
    }
}
