using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Models;

public class FixedAsset
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Model { get; set; }
    public string? SerialNumber { get; set; }
    public string? AssetCode { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime ExpirationDate { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public ICollection<AssetHistory>? AssetHistories { get; set; } 
}
