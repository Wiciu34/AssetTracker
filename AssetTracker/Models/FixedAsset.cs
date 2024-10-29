using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Models;

public class FixedAsset
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Nazwa jest wymagana!")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Model jest wymagany!")]
    public string? Model { get; set; }
    [Required(ErrorMessage = "Numer seryjny jest wymagany")]
    public string? SerialNumber { get; set; }
    [Required(ErrorMessage = "Kod zasobu jest wymagany!")]
    public string? AssetCode { get; set; }
    [DataType(DataType.DateTime)]
    [Required(ErrorMessage = "Data ważności jest wymagana!")]
    public DateTime? ExpirationDate { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime? AssignmentDate {  get; set; }
    [DataType(DataType.DateTime)]
    public DateTime? ReturnDate { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
