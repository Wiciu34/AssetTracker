using AssetTracker.Models;

namespace AssetTracker.Interfaces;

public interface IDashboardRepository
{
    Task<int> GetEmployyeCountAsync();
    Task<int> GetAssetCountAsync();
    Task<int> GetUnassignedAssetsCountAsync();
    Task<int> GetAssignedAssetsCountAsync();
    Task<List<Employee>> GetEmployeeWithTheMostAssets();
}
