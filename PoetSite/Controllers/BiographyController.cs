using Microsoft.AspNetCore.Mvc;
using PoetSite.Databases;

namespace PoetSite.Controllers;

public class BiographyController : Controller
{
    private readonly AppDbContext _context;

    public BiographyController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var bio = _context.Biographies.FirstOrDefault();
        return View(bio);
    }
}