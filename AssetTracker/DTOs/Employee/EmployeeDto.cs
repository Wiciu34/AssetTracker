using AssetTracker.Data.Enum;
using AssetTracker.DTOs.FixedAsset;

namespace AssetTracker.DTOs.Employee;

public class EmployeeDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Position { get; set; }
    public Workplace? Workplace { get; set; }
    public string? Email { get; set; }
}
