using System.ComponentModel.DataAnnotations;

namespace dotnetBB.Models.Request;

public class RegistrationModel
{
    [Required] 
    public string Username { get; set; } = null!;
        
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required] 
    [MaxLength(128)]
    public string Password { get; set; } = null!;

    [Required] 
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = null!;
}