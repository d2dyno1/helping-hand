using PomocKolezenska.Models.Database;

namespace PomocKolezenska.Components.Pages;

public partial class Tutors
{
    private List<Tutor> TutorsList { get; set; }
    private List<Subject> Subjects { get; set; }

    private int _subjectFilter = -1;

    public int SubjectFilter
    {
        get => _subjectFilter;
        set
        {
            _subjectFilter = value;
            RefreshUsers();
        }
    }

    protected override Task OnInitializedAsync()
    {
        Subjects = ApplicationDbContext.Subjects.ToList();
        
        RefreshUsers();
        return base.OnInitializedAsync();
    }

    private void RefreshUsers()
    {
        var users = ApplicationDbContext.Users.Where(x => ApplicationDbContext.UserSubjects.Any(y => y.UsersId == x.Id))
            .ToList();
        if (SubjectFilter != -1)
        {
            users = ApplicationDbContext.Users.Where(x => ApplicationDbContext.UserSubjects.Any(y => y.UsersId == x.Id && y.SubjectsId == SubjectFilter))
                .ToList();
        }

        TutorsList = users.Select(x => new Tutor
        {
            Username = x.Username,
            Phone = x.ContactPhoneNumber,
            Email = x.ContactEmailAddress,
            UserImageBase64 = x.UserImageBase64,
            Subjects = ApplicationDbContext.UserSubjects.Where(y => y.UsersId == x.Id)
                .Select(y => ApplicationDbContext.Subjects.First(z => z.Id == y.SubjectsId).Name).ToList()
        }).ToList();
    }

    private sealed class Tutor
    {
        public string Username { get; set; }
        public string UserImageBase64 { get; set; }
        public List<string> Subjects { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
