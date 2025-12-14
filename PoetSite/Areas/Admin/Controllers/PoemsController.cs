using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoetSite.Databases;
using PoetSite.Models;

namespace PoetSite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class PoemsController : Controller
{
    private readonly AppDbContext _context;


    public PoemsController(AppDbContext context)
    {
        _context = context;
    }


    public IActionResult Index()
    {
        return View(_context.Poems.ToList());
    }


    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Create(Poem poem)
    {
        if (!ModelState.IsValid)
            return View(poem);


        _context.Poems.Add(poem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }


    public IActionResult Edit(int id)
    {
        var poem = _context.Poems.Find(id);
        if (poem == null) return NotFound();
        return View(poem);
    }


    [HttpPost]
    public IActionResult Edit(Poem poem)
    {
        if (!ModelState.IsValid)
            return View(poem);


        _context.Poems.Update(poem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }


    public IActionResult Delete(int id)
    {
        var poem = _context.Poems.Find(id);
        if (poem == null) return NotFound();


        _context.Poems.Remove(poem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}