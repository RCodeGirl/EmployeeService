using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesService.Models
{
    public class Passport
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } =  string.Empty;
        public string Number { get; set; }= string.Empty;
        public Employee Employee { get; set; }       
    }
}
