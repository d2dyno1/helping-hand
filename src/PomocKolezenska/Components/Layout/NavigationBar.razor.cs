namespace PomocKolezenska.Components.Layout;

public partial class NavigationBar
{
    private StaticModal LoginModal { get; set; } = null!;
    private StaticModal RegisterModal { get; set; } = null!;
    
    private string? UserImage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var user = await UserService.GetUser(AuthenticationStateProvider);
        UserImage = user?.UserImageBase64;
    }

    private Task OnLoginClick()
    {
        return LoginModal.ShowAsync();
    }

    private Task OnRegisterClick()
    {
        return RegisterModal.ShowAsync();
    }
}
