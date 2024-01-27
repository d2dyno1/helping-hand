namespace dotnetBB.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    
    public bool IsAdmin { get; set; }
}