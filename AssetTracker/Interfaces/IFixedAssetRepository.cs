﻿using AssetTracker.Models;

namespace AssetTracker.Interfaces;

public interface IFixedAssetRepository
{
    Task<IEnumerable<FixedAsset>> GetAllAssetsAsync(bool freeAssets = false);
    Task<FixedAsset> GetAssetByIdAsync(int id);
    Task CreateFixedAsset(FixedAsset fixedAsset);
    Task UpdateFixedAsset(FixedAsset fixedAsset);
    Task DeleteFixedAssetAsync(int id);
    Task<bool> IsSerialNumberInUse(string serialNumber);
    Task<bool> IsAssetCodeInUse(string assetCode);
    Task AddAssetsToEmployee(List<FixedAsset> assets, int employeeId);
    Task RemoveAssetFromEmployee(FixedAsset asset, int employeeId);
}
