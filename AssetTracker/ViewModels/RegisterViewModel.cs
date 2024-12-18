using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AssetTracker.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
    public string? ConfirmPassword { get; set; }
    public IEnumerable<SelectListItem>?  EmployeeEmails { get; set; }
}
