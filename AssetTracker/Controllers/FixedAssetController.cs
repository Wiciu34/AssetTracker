using AssetTracker.DTOs.FixedAsset;
using AssetTracker.Interfaces;
using AssetTracker.Mappers;
using AssetTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    public async Task<JsonResult> GetAssets(bool freeAssets = false)
    {
        var assets = await _fixedAssetRepository.GetAllAssetsAsync(freeAssets);

        var assetsDto = assets.Select(a => a.ToFixedAssetDto()).ToList();

        return Json(new {data = assetsDto});
    }

    public async Task<IActionResult> Details(int id)
    {
        var asset = await _fixedAssetRepository.GetAssetByIdAsync(id);

        if (asset == null)
        {
            return NotFound();
        }

        return View(asset.ToFixedAssetDto());
    }

    [HttpGet]
    public async Task<IActionResult> GetAssetPartialView(int id)
    {
        var asset = await _fixedAssetRepository.GetAssetByIdAsync(id);

        return PartialView("_FixedAssetPartialView", asset.ToFixedAssetDto());
    }

    [HttpGet]
    public async Task<JsonResult> GetAsset(int id)
    {
        var asset = await _fixedAssetRepository.GetAssetByIdAsync(id);

        return Json(new {data = asset.ToFixedAssetDto()});
    }

    [HttpPost]
    public async Task<JsonResult> CreateAsset(CreateUpdateAssetDto assetDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var asset = assetDto.FromCreateUpdateAsset();
                await _fixedAssetRepository.CreateFixedAsset(asset);
                return Json(new { success = true});
            }
            catch(DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("IX_FixedAssets_SerialNumber") == true)
                {
                    ModelState.AddModelError("SerialNumber", "Numer seryjny jest już w użyciu.");
                }

                if(ex.InnerException?.Message.Contains("IX_FixedAssets_AssetCode") == true)
                {
                    ModelState.AddModelError("AssetCode", "Kod zasobu jest już w użyciu.");
                }

                if (!ex.InnerException.Message.Contains("IX_FixedAssets_SerialNumber") &&
                !ex.InnerException.Message.Contains("IX_FixedAssets_AssetCode"))
                {
                    ModelState.AddModelError("", "Wystąpił błąd podczas zapisu danych.");
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
    public async Task<JsonResult> EditAsset(CreateUpdateAssetDto assetDto, int assetId)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _fixedAssetRepository.UpdateFixedAsset(assetDto, assetId);
                return Json(new { success = true });
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("IX_FixedAssets_SerialNumber") == true)
                {
                    ModelState.AddModelError("SerialNumber", "Numer seryjny jest już w użyciu.");
                }

                if (ex.InnerException?.Message.Contains("IX_FixedAssets_AssetCode") == true)
                {
                    ModelState.AddModelError("AssetCode", "Kod zasobu jest już w użyciu.");
                }

                if (!ex.InnerException.Message.Contains("IX_FixedAssets_SerialNumber") &&
                !ex.InnerException.Message.Contains("IX_FixedAssets_AssetCode"))
                {
                    ModelState.AddModelError("", "Wystąpił błąd podczas zapisu danych.");
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
    public async Task<JsonResult> DeleteAsset(int id)
    {
        try
        {
            await _fixedAssetRepository.DeleteFixedAssetAsync(id);
            return Json(new { success = true });
        }
        catch(Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
        
    }

    [HttpPost]
    public async Task<JsonResult> AddAssetsToEmployee(List<int> assetsIds, int employeeId)
    {
        List<FixedAsset> assetsToAdd = new List<FixedAsset>();

        foreach (int assetId in assetsIds)
        {
            var asset = await _fixedAssetRepository.GetAssetByIdAsync(assetId);
            asset.AssignmentDate = DateTime.UtcNow;
            assetsToAdd.Add(asset);
        }

        await _fixedAssetRepository.AddAssetsToEmployee(assetsToAdd, employeeId);

        return Json(new { success = true, data = employeeId});
    }

    [HttpPost]
    public async Task<JsonResult> RemoveAssetFromEmployee(int assetId, int employeeId)
    {
        var assetToRemove = await _fixedAssetRepository.GetAssetByIdAsync(assetId);

        assetToRemove.ReturnDate = DateTime.UtcNow;
        assetToRemove.AssignmentDate = null;

        await _fixedAssetRepository.RemoveAssetFromEmployee(assetToRemove, employeeId);

        return Json(new { success = true, data = employeeId });
    }
}
