using AssetTracker.Models;

namespace AssetTracker.Interfaces;

public interface IAssetHistoryRepository
{
    Task AddToHistory(List<FixedAsset> assets, int employeeId);
    Task EndHistory(FixedAsset asset);
}
