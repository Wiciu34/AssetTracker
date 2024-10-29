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
            Workplace = employee.Workplace.ToString(),
            Email = employee.Email,
            FixedAssets = employee.FixedAssets?.Select(f => f.ToFixedAssetDto()).ToList(),
        };
    }
}
