using AssetTracker.Interfaces;
using AssetTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracker.Controllers;

public class DashboardController : Controller
{
    private readonly IDashboardRepository _repository;
    public DashboardController(IDashboardRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<JsonResult> AssetsForPieChart()
    {
        int assignedCount = await _repository.GetAssignedAssetsCountAsync();
        int unassignedCount = await _repository.GetUnassignedAssetsCountAsync();

        return Json(new {
            Assigned = assignedCount,
            unassigned = unassignedCount,
        });
    }

    [HttpGet]
    public async Task<JsonResult> EmployeesWithTheMostAssets()
    {
        List<Employee> list = await _repository.GetEmployeeWithTheMostAssets();

        var assetCounts = list.Select(emp => new
        {
            EmployeeName = emp.Name + " " + emp.Surname,
            AssetCount = emp.FixedAssets?.Count(),

        }).ToList();

        return Json(assetCounts);
    }

    [HttpGet]
    public async Task<JsonResult> NewlyGrantedAssets()
    {
        var assetsHistories = await _repository.GetNewlyGrantedAssets();

        var grantedAssets = assetsHistories.Select(ah => new
        {
            Name = ah.Asset?.Name,
            Model = ah.Asset?.Model,
            AssetStartDate = ah.StartDate,

        }).ToList();

        return Json(grantedAssets);
    }
}
