using AssetTracker.DTOs.Dashboard;
using AssetTracker.Interfaces;
using AssetTracker.Mappers;
using AssetTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.WebSockets;

namespace AssetTracker.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDashboardRepository _repository;

    public HomeController(ILogger<HomeController> logger, IDashboardRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var assetsHistories = await _repository.GetNewlyGrantedAssets();

        var grantedAssets = assetsHistories.Select(a => a.ToGrantedAssetDto()).ToList();

        var counts = new Dashboard
        {
            EmployeesCount = await _repository.GetEmployyeCountAsync(),
            AssetsCount = await _repository.GetAssetCountAsync(),
            NewlyGrantedAssets = grantedAssets
        };
        
        return View(counts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
