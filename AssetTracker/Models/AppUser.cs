using Microsoft.AspNetCore.Identity;

namespace AssetTracker.Models;

public class AppUser : IdentityUser
{
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
