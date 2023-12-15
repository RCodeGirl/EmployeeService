using EmployeesService.Api.Services.Interfaces;
using EmployeesService.Models;
using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using EmployeesService.Api.Dtos;
using System.Numerics;

namespace EmployeesService.Api.Services
{
    public class DepartamentService : IDepartamentService
    {
        private readonly IDbConnection _dbConnection;
        public DepartamentService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public int Create(Department department)
        {       
            var sqlDepartment = "INSERT INTO Departments (Name, Phone) VALUES (@Name, @Phone); SELECT CAST(SCOPE_IDENTITY() as int)";
            var departmentId = _dbConnection.ExecuteScalar<int>(sqlDepartment, new { department.Name, department.Phone });
            return departmentId;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        //public int GetDepartmentByEmpId(int employeeId)
        //{

        //    var department = _dbConnection.QueryFirstOrDefault<Department>(
        //        "SELECT Departments.* " +
        //        "FROM Departments " +
        //        "JOIN EmployeeDepartments ON Departments.Id = EmployeeDepartments.DepartmentId " +
        //        "WHERE EmployeeDepartments.EmployeeId = @EmployeeId",
        //        new { EmployeeId = employeeId });
        //    return department!.Id;

        //}

        public void Update(int depId, DepartmentDto department)
        {
            string updatePassportSql = @"UPDATE Departments  SET  Name = @Name,  Phone = @Phone  WHERE Id = @DeppartmentId;";
            _dbConnection.Execute(updatePassportSql, new { Name = department.Name, Phone = department.Phone, DeppartmentId = depId });
        }
    }

       
}
