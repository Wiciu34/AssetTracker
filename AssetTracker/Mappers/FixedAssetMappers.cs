using AssetTracker.DTOs.FixedAsset;
using AssetTracker.Models;

namespace AssetTracker.Mappers;

public static class FixedAssetMappers
{
    public static FixedAssetDto ToFixedAssetDto(this FixedAsset fixedAsset)
    {
        return new FixedAssetDto
        {
            Id = fixedAsset.Id,
            Name = fixedAsset.Name,
            Model = fixedAsset.Model,
            SerialNumber = fixedAsset.SerialNumber,
            AssetCode = fixedAsset.AssetCode,
            ExpirationDate = fixedAsset.ExpirationDate,
            AssignmentDate = fixedAsset.AssignmentDate,
            ReturnDate = fixedAsset.ReturnDate,
            EmployeeId = fixedAsset.EmployeeId
        };
    }

    public static FixedAsset FromCreateUpdateAssetDto(this CreateUpdateAssetDto assetDto)
    {
        return new FixedAsset
        {
            Name = assetDto.Name,
            Model = assetDto.Model,
            SerialNumber = assetDto.SerialNumber,
            AssetCode = assetDto.AssetCode,
            ExpirationDate = assetDto.ExpirationDate,
        };
    }
}
