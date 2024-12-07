using AssetTracker.DTOs.AssetHistory;
using AssetTracker.DTOs.FixedAsset;
using AssetTracker.Models;

namespace AssetTracker.ViewModels;

public class FixedAssetViewModel
{
    public FixedAssetDto? Asset { get; set; }
    public PaginatedList<AssetHistoryDto>? AssetHistories { get; set; }
}
