using AssetTracker.Data;
using AssetTracker.DTOs.FixedAsset;
using AssetTracker.Interfaces;
using AssetTracker.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Repository;

public class FixedAssetRepository : IFixedAssetRepository
{
    private readonly AppDbContext _context;
    public FixedAssetRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<FixedAsset>> GetAllAssetsAsync(bool freeAssets = false)
    {
        if(_context.FixedAssets == null)
        {
            throw new InvalidOperationException("Entity set 'FixedAssets' is null");
        }

        if(freeAssets)
        {
           return await _context.FixedAssets.Where(fa => fa.EmployeeId == null).ToListAsync();
        }

        return await _context.FixedAssets.ToListAsync();
    }
    public async Task<FixedAsset> GetAssetByIdAsync(int id)
    {
        if (_context.FixedAssets == null)
        {
            throw new InvalidOperationException("Entity set 'FixedAssets' is null");
        }

        var fixedAsset = await _context.FixedAssets.Include(ah => ah.AssetHistories).ThenInclude(e => e.Employee).SingleOrDefaultAsync(fa => fa.Id == id);

        if (fixedAsset == null)
        {
            throw new InvalidOperationException("Cannot find this asset");
        }

        return fixedAsset;
    }
    public async Task CreateFixedAsset(FixedAsset fixedAsset)
    {
        _context.FixedAssets.Add(fixedAsset);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFixedAsset(CreateUpdateAssetDto fixedAssetDto, int assetId)
    {
        var existingAsset = await _context.FixedAssets.FirstOrDefaultAsync(fa => fa.Id == assetId);

        if (existingAsset == null)
        {
            throw new InvalidOperationException("Asset does not exsit");
        }

        existingAsset.Name = fixedAssetDto.Name;
        existingAsset.Model = fixedAssetDto.Model;
        existingAsset.SerialNumber = fixedAssetDto.SerialNumber;
        existingAsset.AssetCode = fixedAssetDto.AssetCode;
        existingAsset.ExpirationDate = fixedAssetDto.ExpirationDate;
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFixedAssetAsync(int id)
    {
        var fixedAsset = await _context.FixedAssets.SingleOrDefaultAsync(fa => fa.Id == id);

        if(fixedAsset == null)
        {
            throw new InvalidOperationException("Cannot find this asset");
        }

        _context.FixedAssets.Remove(fixedAsset);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsSerialNumberInUse(string serialNumber)
    {
        return await _context.FixedAssets.AnyAsync(a => a.SerialNumber == serialNumber);
    }

    public async Task<bool> IsAssetCodeInUse(string assetCode)
    {
        return await _context.FixedAssets.AnyAsync(a => a.AssetCode == assetCode);
    }

    public async Task AddAssetsToEmployee(List<FixedAsset> assets, int employeeId)
    {
        var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Id == employeeId);

        if(employee == null)
        {
            throw new InvalidOperationException("Cannot find this employee");
        }

        foreach (FixedAsset asset in assets)
        {
            asset.EmployeeId = employee?.Id;
            asset.Employee = employee;
            employee?.FixedAssets?.Add(asset);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemoveAssetFromEmployee(FixedAsset asset, int employeeId)
    {
        var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Id == employeeId);

        if (employee == null)
        {
            throw new InvalidOperationException("Cannot find this employee");
        }

        asset.EmployeeId = null;
        asset.Employee = null;

        employee.FixedAssets?.Remove(asset);
        
        await _context.SaveChangesAsync();
    }
}
