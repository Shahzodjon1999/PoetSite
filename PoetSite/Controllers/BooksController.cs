using Microsoft.AspNetCore.Mvc;
using PoetSite.Databases;

namespace PoetSite.Controllers;

public class BooksController : Controller
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Books.ToList());
    }
}