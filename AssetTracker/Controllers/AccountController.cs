using AssetTracker.Interfaces;
using AssetTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssetTracker.Controllers;

public class AccountController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    public AccountController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public async Task<IActionResult> Register()
    {
        var employees = await _employeeRepository.GetAllEmployeesAsync();

        var selectList = employees.Select(e => new SelectListItem
        {
            Value = e.Email,
            Text = e.Email
        })
        .ToList();

        var model = new RegisterViewModel()
        {
            EmployeeEmails = selectList
        };

        return View(model);
    }
}
