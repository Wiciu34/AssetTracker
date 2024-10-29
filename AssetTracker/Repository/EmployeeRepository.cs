using AssetTracker.Data;
using AssetTracker.DTOs.Employee;
using AssetTracker.Interfaces;
using AssetTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;
    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        if (_context.Employees == null) {
            throw new InvalidOperationException("Entity set 'Employees' is null");
        }

        return await _context.Employees.ToListAsync();
    }
    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        if (_context.Employees == null)
        {
            throw new InvalidOperationException("Entity set 'Employees' is null");
        }

        var employee = await _context.Employees.Include(fa => fa.FixedAssets).SingleOrDefaultAsync(e => e.Id == id); ;

        if (employee == null)
        {
            throw new InvalidOperationException("Employee does not exist");
        }

        return employee;
    }
    public async Task CreateEmployeeAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();  
    }
    public async Task UpdateEmployeeAsync(CreateUpdateEmployeeDto employeeDto, int employeeId)
    {
        var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

        if (existingEmployee == null)
        {
            throw new InvalidOperationException("Employee does not exist");
        }

        existingEmployee.Name = employeeDto.Name;
        existingEmployee.Surname = employeeDto.Surname;
        existingEmployee.Position = employeeDto.Position;
        existingEmployee.Workplace = employeeDto.Workplace;
        existingEmployee.Email = employeeDto.Email;

        await _context.SaveChangesAsync();
    }
    public async Task DeleteEmployeeAsync(int id)
    {
        var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        
        if(existingEmployee != null) 
        {
            _context.Remove(existingEmployee);
            await _context.SaveChangesAsync();
        }
    }
}
