using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Models;

public class FixedAsset
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Model { get; set; }
    [Required]
    public string? SerialNumber { get; set; }
    [Required]
    public string? AssetCode { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
