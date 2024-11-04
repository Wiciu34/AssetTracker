using AssetTracker.Data.Enum;
using AssetTracker.DTOs.Employee;
using AssetTracker.Helpers;
using AssetTracker.Interfaces;
using AssetTracker.Mappers;
using Microsoft.AspNetCore.Mvc;

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

            var employeesDto = employees.Select(e => e.ToEmployeeDto()).ToList();

            return Json(new {data = employeesDto});
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            ViewBag.WorkplaceList = EnumHelper.GetSelectListItems<Workplace>();

            return View(employee.ToEmployeeDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeePartialView(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            return PartialView("_EmployeePartialView", employee.ToEmployeeDto());
        }

        [HttpGet]
        public async Task<JsonResult> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            return Json(new {data = employee.ToEmployeeDto()});
        }

        [HttpPost]
        public async Task<JsonResult> CreateEmployee(CreateUpdateEmployeeDto employeeDto)
        {
            if(ModelState.IsValid)
            {
                var employee = employeeDto.ToEmployeeFromCreateUpdateDto();
                await _employeeRepository.CreateEmployeeAsync(employee);

                return Json(new { success = true});
            }

            var errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return Json(new {success = false, errors = errors});
        }

        [HttpPost]
        public async Task<JsonResult> EditEmployee(CreateUpdateEmployeeDto employeeDto, int employeeId)
        {
            if(ModelState.IsValid)
            {
                await _employeeRepository.UpdateEmployeeAsync(employeeDto, employeeId);
                return Json(new { success = true});
            }

            var errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
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
