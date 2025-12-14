namespace PoetSite.Models;

public class GalleryImage
{
    public int Id { get; set; }
    public string ImagePath { get; set; } = null!;
    public string Caption { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}


