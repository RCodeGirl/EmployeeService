using EmployeesService.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace EmployeesService.Api.Dtos
{
    public class DepartmentDto
    {
        [DefaultValue("")]
        public string? Name { get; set; }

        [DefaultValue("")]
        public string? Phone { get; set; } 
       
    }
}
