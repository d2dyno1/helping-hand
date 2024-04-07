namespace PomocKolezenska.Models.Database;

public class Question
{
    public Guid Id { get; set; }
    public User Author { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string Title { get; set; }
    public string Content { get; set; }
    
    public ICollection<QuestionReply> Replies { get; } = new List<QuestionReply>();
}