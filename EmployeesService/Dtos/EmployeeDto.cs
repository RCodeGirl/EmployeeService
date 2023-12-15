using EmployeesService.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace EmployeesService.Api.Dtos
{
    public class EmployeeDto
    {
        [DefaultValue("")]
        public string? Name { get; set; }

        [DefaultValue("")]
        public string? Surname { get; set; }

        [DefaultValue("")]
        public string? Phone { get; set; }
        public int? CompanyId { get; set; }
        public PassportDto Passport { get; set; }       
        public DepartmentDto Department { get; set; }       
    }
}
