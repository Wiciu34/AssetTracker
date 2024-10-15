using AssetTracker.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracker.Controllers;

public class FixedAssetController : Controller
{
    private readonly IFixedAssetRepository _fixedAssetRepository;

    public FixedAssetController(IFixedAssetRepository fixedAssetRepository)
    {
        _fixedAssetRepository = fixedAssetRepository;
    }
    public async Task<IActionResult> Index()
    {
        var fixedAssets = await _fixedAssetRepository.GetAllAssetsAsync();
        return View(fixedAssets);
    }
}
