using Microsoft.AspNetCore.Mvc;
using PoetSite.Databases;

namespace PoetSite.Controllers;

public class GalleryController : Controller
{
    private readonly AppDbContext _context;

    public GalleryController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.GalleryImages.ToList());
    }
}