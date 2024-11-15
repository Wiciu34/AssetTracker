using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Models;

public class AssetHistory
{
    [Key]
    public int Id { get; set; }
    public int AssetId { get; set; }
    public FixedAsset? Asset { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
