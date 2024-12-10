using AssetTracker.DTOs.Employee;
using AssetTracker.Models;

namespace AssetTracker.Mappers;

public static class EmployeeMappers
{
    public static EmployeeDto ToEmployeeDto(this Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Position = employee.Position,
            Workplace = employee.Workplace,
            Email = employee.Email,
        };
    }

    public static Employee ToEmployeeFromCreateUpdateDto(this CreateUpdateEmployeeDto employeeDto)
    {
        return new Employee
        {
            Name = employeeDto.Name,
            Surname = employeeDto.Surname,
            Position = employeeDto.Position,
            Workplace = employeeDto.Workplace,
            Email = employeeDto.Email,
        };
    }
}
