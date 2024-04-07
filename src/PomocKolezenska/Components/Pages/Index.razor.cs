using Microsoft.AspNetCore.Components;
using PomocKolezenska.Models.Database;
using PomocKolezenska.Models.Request;

namespace PomocKolezenska.Components.Pages;

public partial class Index
{
    [SupplyParameterFromForm]
    private NewQuestionModel Model { get; set; } = new();
    
    private StaticModal NewQuestionModal { get; set; }

    private List<Question> AllQuestions { get; set; } = new List<Question>();
    public List<Question> UserQuestions { get; set; } = new List<Question>();

    protected override async Task OnInitializedAsync()
    {
        AllQuestions.AddRange(ApplicationDbContext.Questions);
        
        var user = await UserService.GetUser(ApplicationDbContext, AuthenticationStateProvider);
        if (user is null)
            return;
        UserQuestions.AddRange(user.Questions);
    }

    private async Task OnSubmitAsync()
    {
        var user = await UserService.GetUser(ApplicationDbContext, AuthenticationStateProvider);
        if (user is null)
            return;
        
        var question = new Question { Content = Model.Content, Title = Model.Title };
        user.Questions.Add(question);
        ApplicationDbContext.Update(user);
        await ApplicationDbContext.SaveChangesAsync();
        NavigationManager.Refresh(true);
    }

    private Task OpenNewQuestionModalAsync()
    {
        return NewQuestionModal.ShowAsync();
    }
}

