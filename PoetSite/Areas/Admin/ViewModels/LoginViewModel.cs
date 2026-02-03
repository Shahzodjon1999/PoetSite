using System.ComponentModel.DataAnnotations;

namespace PoetSite.Areas.Admin.ViewModels;

public class LoginViewModel
{
    [Required]
    public string UserName { get; set; }
            
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}