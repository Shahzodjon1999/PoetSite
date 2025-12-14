namespace PoetSite.Models;

public class Poem
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Language { get; set; } = "TJ"; // TJ / RU / EN    
}
