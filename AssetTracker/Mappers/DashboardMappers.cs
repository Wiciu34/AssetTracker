using AssetTracker.DTOs.Dashboard;
using AssetTracker.Models;

namespace AssetTracker.Mappers;

public static class DashboardMappers
{
    public static GrantedAsset FromAssetHistoryToGrantedAsset(this AssetHistory assetHistory)
    {
        return new GrantedAsset
        {
            Name = assetHistory.Asset?.Name,
            Model = assetHistory.Asset?.Model,
            AssetStartDate = assetHistory.StartDate
        };
    }
}
