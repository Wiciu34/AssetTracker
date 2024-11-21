namespace AssetTracker.Interfaces;

public interface IDashboardRepository
{
    Task<int> GetEmployyeCountAsync();
    Task<int> GetAssetCountAsync();
}
