using AssetTracker.DTOs.Employee;
using AssetTracker.Models;

namespace AssetTracker.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task CreateEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(CreateUpdateEmployeeDto employeeDto, int employeeId);
    Task DeleteEmployeeAsync(int id);
}
