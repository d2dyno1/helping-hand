namespace PomocKolezenska.Models.Database;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<User> Users { get; } = new List<User>();
}
