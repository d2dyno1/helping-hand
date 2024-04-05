using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PomocKolezenska.Components;

public partial class StaticModal
{
    [Parameter]
    public string Title { get; set; }
    
    [Parameter]
    public ModalSize Size { get; set; }
    
    [Parameter]
    public RenderFragment BodyTemplate { get; set; }
    
    private Guid Guid { get; } = Guid.NewGuid();

    private string? SizeClass => Size switch
    {
        ModalSize.Small => "modal-sm",
        ModalSize.Large => "modal-lg",
        ModalSize.ExtraLarge => "modal-xl",
        _ => null
    };

    public Task ShowAsync()
    {
        return JSRuntime.InvokeVoidAsync("showModal", Guid.ToString()).AsTask();
    }
}
