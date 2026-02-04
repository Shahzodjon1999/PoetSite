using System.ComponentModel.DataAnnotations;

namespace PoetSite.Areas.Admin.ViewModels;

public class LoginViewModel
{
    [Required]
    public string Username { get; set; } = string.Empty;
            
    [Required]
    [DataType(DataType.Password)]
    public string PasswordHash { get; set; } =  string.Empty;
    
    public bool RememberMe { get; set; }
}