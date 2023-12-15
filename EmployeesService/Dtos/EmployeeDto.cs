using EmployeesService.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace EmployeesService.Api.Dtos
{
    public class EmployeeDto
    {
        [DefaultValue(null)]
        public string? Name { get; set; }

        [DefaultValue(null)]
        public string? Surname { get; set; }

        [DefaultValue(null)]
        public string? Phone { get; set; }
        public int? CompanyId { get; set; }
        public PassportDto? Passport { get; set; }       
        public DepartmentDto? Department { get; set; }       
    }
}
