using AssetTracker.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Surname { get; set; }
    [Required]
    public string? Position { get; set; }
    [Required]
    public Workplace Workplace { get; set; }
    public ICollection<FixedAsset>? FixedAssets { get; set; }
}
