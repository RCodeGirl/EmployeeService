using Newtonsoft.Json;
using System.ComponentModel;

namespace EmployeesService.Api.Dtos
{
    public class PassportDto
    {
        [DefaultValue("")]
        public string? Type { get; set; }
        [DefaultValue("")]
        public string? Number { get; set; } 
    }
}
