using EmployeesService.Api.Dtos;
using EmployeesService.Models;

namespace EmployeesService.Api.Services.Interfaces
{
    public interface IDepartamentService   {

        int Create(Department employee);
        void Delete(int id);
        //int GetDepartmentByEmpId(int employeeId);       
        void Update(int id, DepartmentDto employee);

        Department GetById(int id);
    }
}

