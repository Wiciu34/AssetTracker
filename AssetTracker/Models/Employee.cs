using AssetTracker.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Position { get; set; }
    public Workplace Workplace { get; set; }
    public string? Email { get; set; }
    public ICollection<FixedAsset>? FixedAssets { get; set; }
}
