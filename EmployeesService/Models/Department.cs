using System.ComponentModel.DataAnnotations;

namespace EmployeesService.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<Employee> Employees { get; set; }
    }
}
