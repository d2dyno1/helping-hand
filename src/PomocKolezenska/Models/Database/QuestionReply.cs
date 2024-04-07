namespace PomocKolezenska.Models.Database;

public class QuestionReply
{
    public Guid Id { get; set; }
    public Question Question { get; set; }
    public User Author { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string Content { get; set; }
}