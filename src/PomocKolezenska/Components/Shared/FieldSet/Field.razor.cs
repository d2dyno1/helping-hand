using Microsoft.AspNetCore.Components;

namespace PomocKolezenska.Components;

public partial class Field
{
    [Parameter]
    public string? Label { get; set; }
    
    [Parameter]
    public RenderFragment ChildContent { get; set; }
}
