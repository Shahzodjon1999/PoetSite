using Microsoft.AspNetCore.Mvc;
using PoetSite.Databases;
using PoetSite.Models;

namespace PoetSite.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Authorization;

[Area("Admin")]
[Authorize]
public class AudioController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public AudioController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // LIST
    public IActionResult Index()
    {
        return View(_context.AudioPoems.AsEnumerable().ToList());
    }

    // CREATE
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AudioPoem model, IFormFile audioFile)
    {
        
        if (audioFile == null || audioFile.Length == 0)
        {
            ModelState.AddModelError("", "MP3 file is required");
            return View(model);
        }

        var folder = Path.Combine(_env.WebRootPath, "uploads/audio");
        Directory.CreateDirectory(folder);

        var fileName = Guid.NewGuid() + Path.GetExtension(audioFile.FileName);
        var path = Path.Combine(folder, fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await audioFile.CopyToAsync(stream);
        }

        model.AudioPath = "/uploads/audio/" + fileName;

        _context.AudioPoems.Add(model);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    // EDIT
    public IActionResult Edit(int id)
    {
        var poem = _context.AudioPoems.Find(id);
        if (poem == null) return NotFound();
        return View(poem);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(AudioPoem model, IFormFile? audioFile)
    {
        var poem = _context.AudioPoems.Find(model.Id);
        if (poem == null) return NotFound();

        poem.Title = model.Title;

        if (audioFile != null && audioFile.Length > 0)
        {
            var oldPath = Path.Combine(_env.WebRootPath, poem.AudioPath.TrimStart('/'));
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);

            var folder = Path.Combine(_env.WebRootPath, "uploads/audio");
            var fileName = Guid.NewGuid() + Path.GetExtension(audioFile.FileName);
            var newPath = Path.Combine(folder, fileName);

            using var stream = new FileStream(newPath, FileMode.Create);
            await audioFile.CopyToAsync(stream);

            poem.AudioPath = "/uploads/audio/" + fileName;
        }

        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Delete(int id)
    {
        var audio = _context.AudioPoems.Find(id);
        if (audio == null) return NotFound();
        return View(audio);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var audio = _context.AudioPoems.Find(id);
        if (audio == null) return NotFound();

        // 🗑 delete file
        var filePath = Path.Combine(
            _env.WebRootPath,
            audio.AudioPath.TrimStart('/')
        );

        if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);

        _context.AudioPoems.Remove(audio);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
