using System.ComponentModel.DataAnnotations;

namespace PoetSite.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? CoverImage { get; set; }   // /uploads/books/covers/...
    public string? PdfFile { get; set; }       // /uploads/books/pdf/...

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}