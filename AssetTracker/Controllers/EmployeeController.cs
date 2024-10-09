using AssetTracker.Interfaces;
using Microsoft.AspNetCore.Mvc;
//using System.Web.Mvc;

namespace AssetTracker.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();

            var employeesModified = employees.Select(e => new
            {
                e.Id,
                e.Name,
                e.Surname,
                e.Position,
                Workplace = e.Workplace.ToString()
            }).ToList();

            return Json(new {data = employeesModified});
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            return View(employee);

        }

    }
}
