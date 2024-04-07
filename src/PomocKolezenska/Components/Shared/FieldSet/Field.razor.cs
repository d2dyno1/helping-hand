using Microsoft.AspNetCore.Components;

namespace PomocKolezenska.Components;

public partial class Field
{
    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public string Class { get; set; } = string.Empty;
    
    [Parameter]
    public string LabelClass { get; set; } = string.Empty;
    
    [Parameter]
    public RenderFragment ChildContent { get; set; }
}
