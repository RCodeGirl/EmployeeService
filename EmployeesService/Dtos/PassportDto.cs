using Newtonsoft.Json;
using System.ComponentModel;

namespace EmployeesService.Api.Dtos
{
    public class PassportDto
    {
       
        public string? Type { get; set; }
        
        public string? Number { get; set; } 
    }
}
