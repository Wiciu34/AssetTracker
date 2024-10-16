using AssetTracker.Data;
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
    public async Task<IEnumerable<FixedAsset>> GetAllAssetsAsync()
    {
        if(_context.FixedAssets == null)
        {
            throw new InvalidOperationException("Entity set 'FixedAssets' is null");
        }

        return await _context.FixedAssets.ToListAsync();
    }
    public async Task<FixedAsset> GetAssetByIdAsync(int id)
    {
        if (_context.FixedAssets == null)
        {
            throw new InvalidOperationException("Entity set 'FixedAssets' is null");
        }

        var fixedAsset = await _context.FixedAssets.SingleOrDefaultAsync(fa => fa.Id == id);

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

    public async Task UpdateFixedAsset(FixedAsset fixedAsset)
    {
        _context.FixedAssets.Update(fixedAsset);
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
}
