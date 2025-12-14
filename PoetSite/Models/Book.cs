namespace PoetSite.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public string Description { get; set; } = null!;
    public string? CoverImage { get; set; }
}