using AssetTracker.Data;
using AssetTracker.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Repository;

public class DashboardRepository : IDashboardRepository
{
    private readonly AppDbContext _context;
    public DashboardRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<int> GetEmployyeCountAsync()
    {
        return await _context.Employees.CountAsync();
    }
    public async Task<int> GetAssetCountAsync()
    {
        return await _context.FixedAssets.CountAsync();
    }
}
