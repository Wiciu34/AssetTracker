using AssetTracker.Data.Enum;
using AssetTracker.Helpers;
using AssetTracker.Interfaces;
using AssetTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ViewBag.WorkplaceList = EnumHelper.GetSelectListItems<Workplace>();
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

            ViewBag.WorkplaceList = EnumHelper.GetSelectListItems<Workplace>();
            return View(employee);

        }

        [HttpGet]
        public async Task<JsonResult> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            return Json(new {data = employee});
        }

        [HttpPost]
        public async Task<JsonResult> CreateEmployee(Employee employee)
        {
            if(ModelState.IsValid)
            {
                await _employeeRepository.CreateEmployeeAsync(employee);
                return Json(new { success = true, employee = employee });
            }

            var errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return Json(new {success = false, errors = errors});
        }

        [HttpPost]
        public async Task<JsonResult> EditEmployee(Employee employee)
        {
            if(ModelState.IsValid)
            {
                await _employeeRepository.UpdateEmployeeAsync(employee);
                return Json(new { success = true, employee = employee });
            }

            var errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return Json(new { success = false, errors = errors });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return Json(new { success = true });
        }

    }
}
