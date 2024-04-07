namespace PomocKolezenska.Models.Database;

public class UserSubjects
{
    public Guid Id { get; set; }
    public int SubjectsId { get; set; }
    public Guid UsersId { get; set; }
}
