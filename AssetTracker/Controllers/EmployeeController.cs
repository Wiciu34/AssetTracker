using AssetTracker.Data.Enum;
using AssetTracker.DTOs.Employee;
using AssetTracker.DTOs.FixedAsset;
using AssetTracker.Helpers;
using AssetTracker.Interfaces;
using AssetTracker.Mappers;
using AssetTracker.Models;
using AssetTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<EmployeeViewModel> GetEmployeeViewModelAsync(int id, int? pageNumber)
        {
            const int pageSize = 2;

            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return null;
            }

            var employeeAssets = employee.FixedAssets?.Select(a => a.ToFixedAssetDto()).ToList();

            var paginatedAssets = PaginatedList<FixedAssetDto>.Create(employeeAssets, pageNumber ?? 1, pageSize);

            return new EmployeeViewModel
            {
                Employee = employee.ToEmployeeDto(),
                FixedAssets = paginatedAssets
            };
        }

        public async Task<IActionResult> Details(int id, int? pageNumber)
        {
            var viewModel = await GetEmployeeViewModelAsync(id, pageNumber);

            if(viewModel == null)
            {
                return NotFound();
            }

            ViewBag.WorkplaceList = EnumHelper.GetSelectListItems<Workplace>();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeePartialView(int id, int? pageNumber)
        {
            var viewModel = await GetEmployeeViewModelAsync(id, pageNumber);

            if (viewModel == null)
            {
                return NotFound();
            }

            return PartialView("_EmployeePartialView", viewModel);
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
                try
                {
                    var employee = employeeDto.ToEmployeeFromCreateUpdateDto();
                    await _employeeRepository.CreateEmployeeAsync(employee);
                    return Json(new { success = true });
                }
                catch(DbUpdateException ex)
                {
                    if(ex.InnerException?.Message.Contains("IX_Employees_Email") == true)
                    {
                        ModelState.AddModelError("employeeDto.Email", "Email jest już zajęty");
                    }
                }
               
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
                try
                {
                    await _employeeRepository.UpdateEmployeeAsync(employeeDto, employeeId);
                    return Json(new { success = true });
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException?.Message.Contains("IX_Employees_Email") == true)
                    {
                        ModelState.AddModelError("employeeDto.Email", "Email jest już zajęty");
                    }
                }

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
