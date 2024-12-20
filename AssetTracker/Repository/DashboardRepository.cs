﻿using AssetTracker.Data;
using AssetTracker.Interfaces;
using AssetTracker.Models;
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

    public async Task<int> GetUnassignedAssetsCountAsync()
    {
        return await _context.FixedAssets.Where(a => a.EmployeeId == null).CountAsync();
    }

    public async Task<int> GetAssignedAssetsCountAsync()
    {
        return await _context.FixedAssets.Where(a => a.EmployeeId != null).CountAsync();
    }

    public async Task<List<Employee>> GetEmployeeWithTheMostAssets()
    {
        return await _context.Employees
            .Include(a => a.FixedAssets)
            .Where(e => e.FixedAssets.Any())
            .OrderByDescending(e => e.FixedAssets.Count())
            .Take(10)
            .ToListAsync();
    }

    public async Task<List<AssetHistory>> GetNewlyGrantedAssets()
    {
        return await _context.AssetHistories
            .Include(a => a.Asset)
            .Where(ah => ah.EndDate == null)
            .OrderByDescending(ah => ah.StartDate)
            .Take(4)
            .ToListAsync();
    }
}
