using Microsoft.AspNetCore.Mvc;
using PoetSite.Databases;

namespace PoetSite.Controllers;

public class AudioController : Controller
{
    private readonly AppDbContext _context;

    public AudioController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.AudioPoems.ToList());
    }
}