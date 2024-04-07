namespace PomocKolezenska.Models.Database;

public class User
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string UserImageBase64 { get; set; }

    public string Email { get; set; }

    public string HashedPassword { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;
    
    public bool IsAdmin { get; set; }
    
    public string? ContactEmailAddress { get; set; }
    public string? ContactPhoneNumber { get; set; }

    public ICollection<Question> Questions { get; } = new List<Question>();
    public ICollection<QuestionReply> QuestionReplies { get; } = new List<QuestionReply>();
    public ICollection<Subject> Subjects { get; } = new List<Subject>();
}