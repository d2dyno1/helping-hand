using Microsoft.AspNetCore.Components.Forms;
using PomocKolezenska.Authorization;
using PomocKolezenska.Extensions;
using PomocKolezenska.Helpers;

namespace PomocKolezenska.Components.Pages.Account;

public partial class Index
{
    private string? UserImage { get; set; }

    public string UsernameText { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        UsernameText = state.User.GetClaim(ClaimTypes.Username)!;
        await UpdateProfilePictureAsync();
    }

    public async Task ChangeProfileNameAsync()
    {
        return;

        //var user = await UserService.GetUser(AuthenticationStateProvider);
        //if (user is null && user.Username != UsernameText)
        //    return;

        //user.Username = UsernameText;
    }

    public async Task ChangeProfilePictureAsync(InputFileChangeEventArgs e)
    {
        var file = e.File;
        var user = await UserService.GetUser(ApplicationDbContext, AuthenticationStateProvider);
        if (user is null)
            return;

        await using var stream = file.OpenReadStream();
        user.UserImageBase64 = await ImageConversionHelpers.ConvertToBase64Async(stream);
        await UpdateProfilePictureAsync();
        await UserService.SaveAsync();
    }

    private async Task UpdateProfilePictureAsync()
    {
        var user = await UserService.GetUser(ApplicationDbContext, AuthenticationStateProvider);
        UserImage = user?.UserImageBase64;
    }
}
