using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoetSite.Databases;
using PoetSite.Models;

namespace PoetSite.Areas.Admin.Controllers;

[Area("Admin")]
public class BiographyController : Controller
{
    private readonly AppDbContext _context;


    public BiographyController(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IActionResult> Index()
    {
        return View(await _context.Biographies.ToListAsync());
    }


    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(Biography model)
    {
        if (!ModelState.IsValid)
            return View(model);


        _context.Biographies.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Edit(int id)
    {
        var bio = await _context.Biographies.FindAsync(id);
        if (bio == null) return NotFound();
        return View(bio);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(Biography model)
    {
        if (!ModelState.IsValid)
            return View(model);


        _context.Biographies.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        var bio = await _context.Biographies.FindAsync(id);
        if (bio == null) return NotFound();


        _context.Biographies.Remove(bio);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}