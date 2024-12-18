using Microsoft.AspNetCore.Mvc;

namespace AssetTracker.Controllers;

public class AccountController : Controller
{
    public IActionResult Register()
    {
        return View();
    }
}
