namespace AssetTracker.DTOs.Dashboard;

public class Dashboard
{
    public int EmployeesCount { get; set; }
    public int AssetsCount { get; set; }
    public List<GrantedAsset>? NewlyGrantedAssets { get; set; }
}
