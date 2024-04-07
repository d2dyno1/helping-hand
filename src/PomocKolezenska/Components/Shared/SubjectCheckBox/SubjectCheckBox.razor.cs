using Microsoft.AspNetCore.Components;
using PomocKolezenska.Models.Database;

namespace PomocKolezenska.Components;

public partial class SubjectCheckBox
{
    [Parameter]
    public Subject Subject { get; set; }
    
    private bool Checked { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var user = await UserService.GetUser(ApplicationDbContext, AuthenticationStateProvider);
        if (user is null)
            return;

        Checked = ApplicationDbContext.UserSubjects.Any(x => x.UsersId == user.Id && x.SubjectsId == Subject.Id);
    }

    private async Task OnCheckedChangedAsync(bool value)
    {
        var user = await UserService.GetUser(ApplicationDbContext, AuthenticationStateProvider);
        if (user is null)
            return;
        
        if (!value && ApplicationDbContext.UserSubjects.Any(x => x.UsersId == user.Id && x.SubjectsId == Subject.Id))
        {
            ApplicationDbContext.UserSubjects.Remove(
                ApplicationDbContext.UserSubjects.First(x => x.UsersId == user.Id && x.SubjectsId == Subject.Id));
        }
        else if (value && !ApplicationDbContext.UserSubjects.Any(x => x.UsersId == user.Id && x.SubjectsId == Subject.Id))
        {
            var subject = new UserSubjects
                { UsersId = user.Id, SubjectsId = Subject.Id };
            ApplicationDbContext.Attach(subject);
            ApplicationDbContext.UserSubjects.Add(subject);
        }
        
        await ApplicationDbContext.SaveChangesAsync();
    }
}