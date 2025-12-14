using Microsoft.AspNetCore.Mvc;
using PoetSite.Databases;

namespace PoetSite.Controllers;

public class PoemsController : Controller
{
    private readonly AppDbContext _context;

    public PoemsController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var poems = _context.Poems
            .OrderByDescending(p => p.CreatedAt)
            .ToList();

        return View(poems);
    }
}