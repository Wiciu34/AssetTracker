using AssetTracker.Data;
using AssetTracker.Interfaces;
using AssetTracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Repository;

public class AssetHistoryRepository : IAssetHistoryRepository
{
    private readonly AppDbContext _context;
    public AssetHistoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddToHistory(List<FixedAsset> assets, int employeeId)
    {
        List<AssetHistory> history = new List<AssetHistory>();

        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee == null)
        {
            throw new InvalidOperationException("Cannot find this employee");
        }

        foreach (FixedAsset asset in assets)
        {
            var currentAssigment = await _context.AssetHistories.Where(ah => ah.AssetId == asset.Id && ah.EndDate == null).FirstOrDefaultAsync();

            if (currentAssigment != null) 
            {
                throw new InvalidOperationException("Asset you are trying to add is already asign");
            }

            var historyItem = new AssetHistory
            {
                AssetId = asset.Id,
                Asset = asset,
                EmployeeId = employee.Id,
                Employee = employee,
                StartDate = DateTime.UtcNow,
            };

            asset.AssetHistories?.Add(historyItem);
            history.Add(historyItem);
        }

        _context.AssetHistories.AddRange(history);
        await _context.SaveChangesAsync();
    }

    public async Task EndHistory(FixedAsset asset)
    {
        var historyItem = await _context.AssetHistories.Where(ah => ah.AssetId == asset.Id && ah.EndDate == null).FirstOrDefaultAsync();

        if (historyItem == null)
        {
            throw new InvalidOperationException("Cannot find history for this asset");
        }

        historyItem.EndDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();


    }
}
