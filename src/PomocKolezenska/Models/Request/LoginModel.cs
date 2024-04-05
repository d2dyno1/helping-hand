using System.ComponentModel.DataAnnotations;

namespace PomocKolezenska.Models.Request;

public class LoginModel
{
    [Required] 
    public string EmailOrUsername { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}