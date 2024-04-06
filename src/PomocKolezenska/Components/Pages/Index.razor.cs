namespace PomocKolezenska.Components.Pages;

public partial class Index
{
    private StaticModal LoginModal { get; set; } = null!;

    private Task OnLoginClick()
    {
        return LoginModal.ShowAsync();
    }
}

