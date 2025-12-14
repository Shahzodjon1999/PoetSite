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
    
    public IActionResult Details(int id)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);
        if (book == null)
            return NotFound();

        return View(book);
    }
    public IActionResult Read(int id)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);
        if (book == null || string.IsNullOrEmpty(book.PdfFile))
            return NotFound();

        return View(book);
    }
}