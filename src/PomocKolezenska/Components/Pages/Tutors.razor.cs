using PomocKolezenska.Models.Database;

namespace PomocKolezenska.Components.Pages;

public partial class Tutors
{
    private List<Tutor> TutorsList { get; set; }
    public string SubjectFilter { get; set; } = "None";

    protected override Task OnInitializedAsync()
    {
        RefreshUsers();
        return base.OnInitializedAsync();
    }

    private void RefreshUsers()
    {
        var users = ApplicationDbContext.Users.Where(x => ApplicationDbContext.UserSubjects.Any(y => y.UsersId == x.Id))
            .ToList();
        if (SubjectFilter != "None")
        {
            var subjectId = ApplicationDbContext.Subjects.First(x => x.Name == SubjectFilter).Id;
            users = ApplicationDbContext.Users.Where(x => ApplicationDbContext.UserSubjects.Any(y => y.UsersId == x.Id && y.SubjectsId == subjectId))
                .ToList();
        }

        TutorsList = users.Select(x => new Tutor
        {
            Username = x.Username,
            Subjects = ApplicationDbContext.UserSubjects.Where(y => y.UsersId == x.Id)
                .Select(y => ApplicationDbContext.Subjects.First(z => z.Id == y.SubjectsId).Name).ToList()
        }).ToList();


    }

    class Tutor
    {
        public string Username { get; set; }
        public List<string> Subjects { get; set; }
    }
}
