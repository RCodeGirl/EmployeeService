using EmployeesService.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace EmployeesService.Api.Dtos
{
    public class DepartmentDto
    {
        [DefaultValue(null)]
        public string? Name { get; set; } 

        [DefaultValue(null)]
        public string? Phone { get; set; } 
       
    }
}
