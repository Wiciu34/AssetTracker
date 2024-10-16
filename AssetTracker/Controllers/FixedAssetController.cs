﻿using AssetTracker.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracker.Controllers;

public class FixedAssetController : Controller
{
    private readonly IFixedAssetRepository _fixedAssetRepository;

    public FixedAssetController(IFixedAssetRepository fixedAssetRepository)
    {
        _fixedAssetRepository = fixedAssetRepository;
    }
    public IActionResult Index()
    {
        return View();
    }

    public async Task<JsonResult> GetAssets()
    {
        var assets = await _fixedAssetRepository.GetAllAssetsAsync();

        return Json(new {data = assets});
    }

    public async Task<IActionResult> Details(int id)
    {
        var asset = await _fixedAssetRepository.GetAssetByIdAsync(id);

        if (asset == null)
        {
            return NotFound();
        }

        return View(asset);
    }
}
