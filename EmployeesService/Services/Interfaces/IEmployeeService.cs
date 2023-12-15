using EmployeesService.Api.Dtos;
using EmployeesService.Models;

namespace EmployeesService.Api.Services.Interfaces
{
    public interface IEmployeeService
    {

        int Create(Employee employee);
        void Delete(int id);
        Employee GetById(int id);
        List<Employee> GetEmployeesByCompanyId(int companyId);
        List<Employee> GetEmployeesByDepartmentId(int departmentId);
        void Update(int employee, EmployeeDto updateData);

    }
}
