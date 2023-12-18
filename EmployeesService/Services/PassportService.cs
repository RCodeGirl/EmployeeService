using EmployeesService.Api.Services.Interfaces;
using EmployeesService.Models;
using System.Data;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using EmployeesService.Api.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.IdentityModel.Tokens;

namespace EmployeesService.Api.Services
{
    public class PassportService : IPassportService
    {
        private readonly IDbConnection _dbConnection;
        public PassportService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public int Create(Passport passport)
        {
            var sqlPassport = "INSERT INTO Passports (Type, Number) VALUES (@Type, @Number); SELECT CAST(SCOPE_IDENTITY() as int)";
            var pasID = _dbConnection.ExecuteScalar<int>(sqlPassport, new { passport.Type, passport.Number });
            return pasID;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        public int Update(PassportDto passportDto, int passId)
        {
            var passport = GetById(passId);
            passport.Type = string.IsNullOrEmpty(passportDto.Type)? passport.Type: passportDto.Type;
            passport.Number = string.IsNullOrEmpty(passportDto.Number) ? passport.Number: passportDto.Number;
            string updatePassportSql = @"UPDATE Passports  SET  Type = @Type,  Number = @Number  WHERE Id = @PassportId;";

            return _dbConnection.Execute(updatePassportSql, new { Type = passport.Type, Number = passport.Number, PassportId = passId });
        }

        public void Update(int passportId, int employeeId)
        {
            string updatePassportSql = @"UPDATE Passports  SET  EmployeeId=@EmployeeId    WHERE Id = @PassportId;";

             _dbConnection.Execute(updatePassportSql, new { EmployeeId = employeeId, PassportId = passportId });
        }


        public Passport GetById(int id)
        {
            string updatePassportSql = "SELECT * FROM Passports WHERE Id = @Id";
            var parameters = new { Id = id };
            return _dbConnection.QueryFirstOrDefault<Passport>(updatePassportSql, parameters)!;
        }
    }
}
