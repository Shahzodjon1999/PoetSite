using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoetSite.Databases;
using PoetSite.Models;

namespace PoetSite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class GalleryController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    
public GalleryController(AppDbContext context, IWebHostEnvironment env)
{
    _context = context;
    _env = env;
}


public IActionResult Index()
{
    return View(_context.GalleryImages.ToList());
}


public IActionResult Create()
{
    return View();
}


[HttpPost]
public async Task<IActionResult> Create(IFormFile image, string title)
{
    if (image == null || image.Length == 0)
    {
        ModelState.AddModelError("", "Image is required");
        return View();
    }


    var uploads = Path.Combine(_env.WebRootPath, "uploads/gallery");
    Directory.CreateDirectory(uploads);


    var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
    var filePath = Path.Combine(uploads, fileName);


    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await image.CopyToAsync(stream);
    }


    var img = new GalleryImage
    {
        ImagePath = "/uploads/gallery/" + fileName,
        Caption = title
    };


    _context.GalleryImages.Add(img);
    _context.SaveChanges();


    return RedirectToAction(nameof(Index));
}


public IActionResult Delete(int id)
{
    var img = _context.GalleryImages.Find(id);
    if (img == null) return NotFound();


    var fullPath = Path.Combine(_env.WebRootPath, img.ImagePath.TrimStart('/'));
    if (System.IO.File.Exists(fullPath))
        System.IO.File.Delete(fullPath);


    _context.GalleryImages.Remove(img);
    _context.SaveChanges();


    return RedirectToAction(nameof(Index));
}
}