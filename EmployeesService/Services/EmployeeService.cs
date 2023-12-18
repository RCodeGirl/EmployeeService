using Dapper;
using EmployeesService.Api.Dtos;
using EmployeesService.Api.Services.Interfaces;
using EmployeesService.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using System.Data;
using System.Web.WebPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeesService.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDepartamentService _departamentService;
        private readonly IPassportService _passportService;
        public EmployeeService(IDbConnection dbConnection, IDepartamentService departamentService, IPassportService passportService)
        {
            _dbConnection = dbConnection;
            _departamentService = departamentService;
            _passportService = passportService;
        }

        public int Create(Employee employee)
        {

            var departmentId = _departamentService.Create(employee.Department);
            var sql = "INSERT INTO Employees (Name, Surname, Phone, CompanyId,  PassportId, DepartmentId) " +
                "VALUES (@Name, @Surname, @Phone,@CompanyId, @PassportId, @DepartmentId); SELECT CAST(SCOPE_IDENTITY() as int)";


            var passId = _passportService.Create(employee.Passport);
            var parametr = new
            {
                Name = employee.Name,
                Surname = employee.Surname,
                Phone = employee.Phone,
                CompanyId = employee.CompanyId,
                PassportId = passId,
                DepartmentId = departmentId
            };

            var employeeId = _dbConnection.ExecuteScalar<int>(sql, parametr);
            return employeeId;

        }

        public void Delete(int id)
        {
            // Получаем информацию о паспорте перед удалением сотрудника
            var employee = GetById(id);

            if (employee is null)
                throw new Exception($"Сотрудник с Id {id} не найден.");

            else
            {
                // Удаляем сотрудника
                _dbConnection.Execute("DELETE FROM Employees WHERE Id = @EmployeeId", new { EmployeeId = id });

                // Проверяем наличие паспорта и удаляем его
                if (employee.PassportId != 0)
                {
                    _dbConnection.Execute("DELETE FROM Passports WHERE Id = @PassportId", new { PassportId = employee.PassportId });
                }
            }

        }

        public Employee GetById(int id)
        {
            var sql = "SELECT * FROM Employees WHERE Id = @EmployeeId";
            var parameters = new { EmployeeId = id };

            return _dbConnection.QueryFirstOrDefault<Employee>(sql, parameters)!;
        }


        /// <summary>
        /// Получение списка сотрудников по Id организации
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<Employee> GetEmployeesByCompanyId(int companyId)
        {

            string sql = @"
                        SELECT
                            e.Id,
                            e.Name,
                            e.Surname,
                            e.Phone,
                            e.CompanyId,
                            e.PassportId,
                            e.DepartmentId,
                            p.Id,
                            p.Type,
                            p.Number,
                            d.Id,
                            d.Name,
                            d.Phone
                        FROM Employees e
                        LEFT JOIN Passports p ON e.PassportId = p.Id
                        LEFT JOIN Departments d ON e.DepartmentId = d.Id
                        WHERE e.CompanyId = @CompanyId;";


            var parameters = new { CompanyId = companyId };

            var result = _dbConnection.Query<Employee, Passport, Department, Employee>(
                sql,
                (employee, passport, department) =>
                {
                    employee.Passport = passport;
                    employee.Department = department;
                    return employee;
                },
                parameters,
                splitOn: "Id,Id"
            ).ToList();

            return result;
        }


        /// <summary>
        /// Получение списка сотрудников по департаменту
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            string sql = @"SELECT
                         e.Id,
                         e.Name,
                         e.Surname,
                         e.Phone,
                         e.CompanyId,
                         e.PassportId,
                         p.Type,
                         p.Id,
                         p.Number,
                         e.DepartmentId,
                         d.Name,
                         d.Id,
                         d.Phone
               FROM Employees e
               LEFT JOIN Passports p ON e.PassportId = p.Id
               LEFT JOIN Departments d ON e.DepartmentId = d.Id
               WHERE e.DepartmentId = @DepartmentId";

            var parameters = new { DepartmentId = departmentId };

            var employees = _dbConnection.Query<Employee, Passport, Department, Employee>(
                sql,
                (employee, passport, department) =>
                {
                    employee.Passport = passport;
                    employee.Department = department;
                    return employee;
                },
                parameters,
                splitOn: "Id,Id"
            ).ToList();

            return employees;
        }


        public void Update(int employeeId, EmployeeDto updatedEmployee)
        {
            var existingEmployee = GetById(employeeId);

            if (existingEmployee == null)
            {
                throw new ArgumentException($"Сотрудник с Id {employeeId} не найден.");
            }

            existingEmployee.Surname = string.IsNullOrEmpty(updatedEmployee.Surname) ? existingEmployee.Surname : updatedEmployee.Surname;
            existingEmployee.Phone = string.IsNullOrEmpty(updatedEmployee.Phone) ? existingEmployee.Phone : updatedEmployee.Phone;
            existingEmployee.CompanyId =updatedEmployee.CompanyId ?? existingEmployee.CompanyId;

            _passportService.Update(updatedEmployee.Passport, existingEmployee.PassportId);

            _departamentService.Update(existingEmployee.DepartmentId, updatedEmployee.Department);


            // Обновляем данные сотрудника
            string updateEmployeeSql = @"
        UPDATE Employees
        SET
            Name = @Name,
            Surname = @Surname,
            Phone = @Phone,
            CompanyId = @CompanyId,
            PassportId = @PassportId,
            DepartmentId = @DepartmentId
        WHERE Id = @Id;";

            _dbConnection.Execute(updateEmployeeSql, existingEmployee);
           
        }


    }
}
