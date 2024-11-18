using AssetTracker.Models;

namespace AssetTracker.Interfaces;

public interface IAssetHistoryRepository
{
    Task addToHistory(List<FixedAsset> assets, int employeeId);
    Task endHistory(int assetId, int employeeId);
}
