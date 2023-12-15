using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesService.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }= "";
        public string Surname { get; set; } = "";
        public string Phone { get; set; } = "";
        public int CompanyId { get; set; }
        public  Passport Passport { get; set; }        
        public int PassportId { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
