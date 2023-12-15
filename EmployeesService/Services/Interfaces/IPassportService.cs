using EmployeesService.Api.Dtos;
using EmployeesService.Models;

namespace EmployeesService.Api.Services.Interfaces
{
    public interface IPassportService
    {
        int Create(Passport passport);        
        void Delete(int id);       
       
        int Update(PassportDto passport, int employeeId);
        void Update(int  passportId, int employeeId);
    }
}
