using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PomocKolezenska.Models.Database;
using PomocKolezenska.Models.Request;

namespace PomocKolezenska.Components.Pages;

public partial class QuestionPage
{
    [SupplyParameterFromForm]
    private QuestionReplyModel Model { get; set; } = new();
    
    [Parameter]
    public string? Id { get; set; }
    
    private Question Question { get; set; }
    private List<QuestionReply> Replies { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Question = ApplicationDbContext.Questions.Include(x => x.Author)
            .First(x => x.Id == Guid.Parse(Id));
        Replies = ApplicationDbContext.QuestionReplies.Include(x => x.Author)
            .Where(x => x.Question.Id == Guid.Parse(Id)).ToList();
    }

    private async Task OnSubmitAsync()
    {
        var user = await UserService.GetUser(ApplicationDbContext, AuthenticationStateProvider);
        if (user is null)
            return;

        var reply = new QuestionReply { Author = user, Content = Model.Content, Question = Question };
        Question.Replies.Add(reply);
        ApplicationDbContext.Update(Question);
        await ApplicationDbContext.SaveChangesAsync();
        NavigationManager.Refresh(true);
    }
}