using Microsoft.AspNetCore.Components;
using PomocKolezenska.Models.Database;

namespace PomocKolezenska.Components.Pages;

public partial class QuestionPage
{
    [Parameter]
    public string? Id { get; set; }
    
    private Question Question { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Question = ApplicationDbContext.Questions.First(x => x.Id == Guid.Parse(Id));
    }
}