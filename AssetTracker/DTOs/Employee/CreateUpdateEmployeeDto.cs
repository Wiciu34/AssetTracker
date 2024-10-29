using AssetTracker.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace AssetTracker.DTOs.Employee;

public class CreateUpdateEmployeeDto
{
    [Required(ErrorMessage = "Imię jest wymagane!")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Nazwisko jest wymagane!")]
    public string? Surname { get; set; }
    [Required(ErrorMessage = "Stanowisko jest wymagane!")]
    public string? Position { get; set; }
    [Required(ErrorMessage = "Miejsce pracy jest wymagane!")]
    public Workplace Workplace { get; set; }
    [Required(ErrorMessage = "Adres email jest wymagany!")]
    [EmailAddress(ErrorMessage = "Adres email musi być prawidłowy!")]
    public string? Email { get; set; }
   
}
