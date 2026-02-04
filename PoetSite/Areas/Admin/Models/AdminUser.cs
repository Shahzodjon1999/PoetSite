using System.ComponentModel.DataAnnotations;

namespace PoetSite.Areas.Admin.Models;

public class AdminUser
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }=string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;
}