namespace PomocKolezenska.Components.Pages;

public partial class Questions
{
    private StaticModal LoginModal { get; set; } = null!;
    private StaticModal RegisterModal { get; set; } = null!;

    private Task OnLoginClick()
    {
        return LoginModal.ShowAsync();
    }

    private Task OnRegisterClick()
    {
        return RegisterModal.ShowAsync();
    }
}
