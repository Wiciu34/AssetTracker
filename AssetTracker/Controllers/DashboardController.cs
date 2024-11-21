using AssetTracker.Interfaces;
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
}
