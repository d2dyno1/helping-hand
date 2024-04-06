using Microsoft.AspNetCore.Components.Forms;
using PomocKolezenska.Helpers;

namespace PomocKolezenska.Components.Pages.Account;

public partial class Index
{
    private string? UserImage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await UpdateProfilePictureAsync();
    }

    public async Task ChangeProfileAsync(InputFileChangeEventArgs e)
    {
        var file = e.File;
        var user = await UserService.GetUser(AuthenticationStateProvider);
        if (user is null)
            return;

        await using var stream = file.OpenReadStream();
        user.UserImageBase64 = await ImageConversionHelpers.ConvertToBase64Async(stream);
        await UpdateProfilePictureAsync();
    }

    private async Task UpdateProfilePictureAsync()
    {
        var user = await UserService.GetUser(AuthenticationStateProvider);
        UserImage = user?.UserImageBase64;
    }
}
