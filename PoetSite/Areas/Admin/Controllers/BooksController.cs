using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoetSite.Databases;
using PoetSite.Models;

namespace PoetSite.Areas.Admin.Controllers;

[Area("Admin")]
public class BooksController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public BooksController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET: Admin/Books
    public async Task<IActionResult> Index()
    {
        return View(await _context.Books.ToListAsync());
    }

    // GET: Admin/Books/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book model, IFormFile coverFile, IFormFile pdfFile)
    {
        if (!ModelState.IsValid)
            return View(model);

        // 📁 folders
        var coverFolder = Path.Combine(_env.WebRootPath, "uploads/books/covers");
        var pdfFolder = Path.Combine(_env.WebRootPath, "uploads/books/pdf");

        Directory.CreateDirectory(coverFolder);
        Directory.CreateDirectory(pdfFolder);

        // 🖼 cover upload
        if (coverFile != null && coverFile.Length > 0)
        {
            var coverName = Guid.NewGuid() + Path.GetExtension(coverFile.FileName);
            var coverPath = Path.Combine(coverFolder, coverName);

            using var stream = new FileStream(coverPath, FileMode.Create);
            await coverFile.CopyToAsync(stream);

            model.CoverImage = "/uploads/books/covers/" + coverName;
        }

        // 📄 pdf upload
        if (pdfFile == null || pdfFile.Length == 0)
        {
            ModelState.AddModelError("", "PDF file is required");
            return View(model);
        }

        var pdfName = Guid.NewGuid() + Path.GetExtension(pdfFile.FileName);
        var pdfPath = Path.Combine(pdfFolder, pdfName);

        using var pdfStream = new FileStream(pdfPath, FileMode.Create);
        await pdfFile.CopyToAsync(pdfStream);

        model.PdfFile = "/uploads/books/pdf/" + pdfName;

        _context.Books.Add(model);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    
    /*[HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book model, IFormFile? coverFile)
    {
        if (ModelState.IsValid)
        {
            if (coverFile != null)
            {
                string uploads = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);
                string filePath = Path.Combine(uploads, coverFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await coverFile.CopyToAsync(stream);
                }
                model.CoverImage = "/uploads/" + coverFile.FileName;
            }

            _context.Books.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }*/

    // GET: Admin/Books/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();
        return View(book);
    }

    // POST: Admin/Books/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book model, IFormFile? coverFile)
    {
        if (id != model.Id) return NotFound();

        if (ModelState.IsValid)
        {
            if (coverFile != null)
            {
                string uploads = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);
                string filePath = Path.Combine(uploads, coverFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await coverFile.CopyToAsync(stream);
                }
                model.CoverImage = "/uploads/" + coverFile.FileName;
            }

            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    // GET: Admin/Books/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();
        return View(book);
    }

    // POST: Admin/Books/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}