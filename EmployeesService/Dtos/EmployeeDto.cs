using EmployeesService.Models;

namespace EmployeesService.Api.Dtos
{
    public class EmployeeDto
    {
        public string Name { get; set; } 
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public PassportDto Passport { get; set; }       
        public DepartmentDto Department { get; set; }       
    }
}
