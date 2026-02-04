using Microsoft.AspNetCore.Mvc;

namespace PoetSite.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Admin") != "true")
            return RedirectToAction("Login", "Account", new { area = "Admin" });

        return View();
    }
}