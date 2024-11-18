namespace AssetTracker.DTOs.AssetHistory;

public class AssetHistoryDto
{
    public int Id { get; set; }
    public string? EmployeeName { get; set; }
    public string? EmployeeSurname { get; set; }
    public int AssetId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
