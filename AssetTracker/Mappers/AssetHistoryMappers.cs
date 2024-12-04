using AssetTracker.DTOs.AssetHistory;
using AssetTracker.Models;

namespace AssetTracker.Mappers;

public static class AssetHistoryMappers
{
    public static AssetHistoryDto ToAssetHistoryDto(this AssetHistory assetHistory)
    {
        return new AssetHistoryDto
        {
            Id = assetHistory.Id,
            AssetId = assetHistory.AssetId,
            EmployeeName = assetHistory.Employee?.Name,
            EmployeeSurname = assetHistory.Employee?.Surname,
            EmployeeWorkplace = assetHistory.Employee.Workplace,
            StartDate = assetHistory.StartDate,
            EndDate = assetHistory.EndDate,
        };
    }
}
