using System.Data;
using AutoMapper;
using Dapper;
using EmployeesService.Api.Dtos;
using EmployeesService.Api.Services;
using EmployeesService.Api.Services.Interfaces;
using EmployeesService.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]

public class EmployeesController : ControllerBase
{
    private readonly IDbConnection _dbConnection;
    private readonly IEmployeeService _employeeService;
    private readonly IPassportService _passportService;
    private readonly IDepartamentService _departamentService;
    private readonly IMapper _mapper;
    public EmployeesController(IDbConnection dbConnection, IEmployeeService employeeService, IMapper mapper, IPassportService passportService, IDepartamentService departamentService)
    {
        _dbConnection = dbConnection;
        _employeeService = employeeService;
        _mapper = mapper;
        _passportService = passportService;
        _departamentService = departamentService;
    }
    [Route("api/[controller]/AddEmployee")]
    [HttpPost]
    public string AddEmployee([FromBody] EmployeeDto employeeDto)
    {
        var employeeModel = _mapper.Map<Employee>(employeeDto);
        try
        {
            var empl = _employeeService.Create(employeeModel);
            return string.Format("Сотрудник успешно добавлен, Id={0} ", empl.ToString());
        }
        catch (Exception ex)
        {

            throw new Exception($"При создании  сотрудника возникла ошбибка: {ex.Message}");
        }
    }

    [Route("api/[controller]/DeleteEmployee")]
    [HttpDelete]
    public IActionResult DeleteEmployee(int id)
    {
        try
        {
            _employeeService.Delete(id);
            return Ok("Удаление cотрудника прошла успешно!");
        }
        catch (Exception ex)
        {

            throw new Exception($"При попытке удаления сотрудника возникла ошбибка: {ex.Message}");
        }
    }

    [Route("api/[controller]/GetEmployeesByCompanyId")]
    [HttpGet]
    public IActionResult GetEmployeesByCompanyId(int companyId)
    {
        var employees = _employeeService.GetEmployeesByCompanyId(companyId);

        if (employees is null || employees.Count == 0)
        {
            return NotFound($"Сотрудники для компании с Id {companyId} не найдены.");
        }
        var employeesDto = _mapper.Map<List<EmployeeDto>>(employees);
        return Ok(employeesDto);
    }

    [Route("api/[controller]/GetEmployeesByDepartmentId")]
    [HttpGet]
    public IActionResult GetEmployeesByDepartmentId(int departmentId)
    {
        var employees = _employeeService.GetEmployeesByDepartmentId(departmentId);

        if (employees is null || employees.Count == 0)
        {          
            return NotFound($"Сотрудники для компании с Id {departmentId} не найдены.");
        }
        var employeesDto = _mapper.Map<List<EmployeeDto>>(employees);

        return Ok(employeesDto);
    }


    [HttpPatch("api/[controller]/UpdateEmployee/{employeeId}")]
    public IActionResult UpdateEmployee(int employeeId, [FromBody] EmployeeDto updateData)
    {
        try
        {
            _employeeService.Update(employeeId, updateData);            
            return Ok($"Сотрудник с Id {employeeId} успешно обновлен.");
        }
        catch (ArgumentException ex)
        {
           
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {            
            return StatusCode(500, $"Произошла ошибка: {ex.Message}");
        }
    }




}
