using AssetTracker.DTOs.AssetHistory;
using AssetTracker.Models;

namespace AssetTracker.DTOs.FixedAsset;

public class FixedAssetDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Model { get; set; }
    public string? SerialNumber { get; set; }
    public string? AssetCode { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int? EmployeeId { get; set; }
    public ICollection<AssetHistoryDto>? AssetHistories { get; set; }
}
