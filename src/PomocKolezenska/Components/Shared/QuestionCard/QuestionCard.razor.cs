using Microsoft.AspNetCore.Components;
using PomocKolezenska.Models.Database;

namespace PomocKolezenska.Components;

public partial class QuestionCard
{
    [Parameter]
    public Question Question { get; set; }
}
