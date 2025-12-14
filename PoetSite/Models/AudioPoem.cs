using System.ComponentModel.DataAnnotations;

namespace PoetSite.Models;

public class AudioPoem
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = String.Empty;
    public string AudioPath { get; set; } = null!;
}
