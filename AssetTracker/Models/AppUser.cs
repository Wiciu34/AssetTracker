using AssetTracker.Data.Enum;
using Microsoft.AspNetCore.Identity;

namespace AssetTracker.Models;

public class AppUser : IdentityUser
{
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public Workplace Workplace { get; set; }
}
