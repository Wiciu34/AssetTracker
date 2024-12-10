using AssetTracker.DTOs.Employee;
using AssetTracker.DTOs.FixedAsset;
using AssetTracker.Models;

namespace AssetTracker.ViewModels;

public class EmployeeViewModel
{
    public EmployeeDto? Employee { get; set; }
    public PaginatedList<FixedAssetDto>? FixedAssets { get; set; }
}
