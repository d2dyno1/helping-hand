namespace PomocKolezenska.Helpers
{
    public static class ImageConversionHelpers
    {
        public static Stream OpenDefaultProfilePicture()
        {
            var rootPath = EnvironmentHelpers.AppInstance.Environment.WebRootPath;
            var filePath = Path.Combine(rootPath, "assets", "profile-picture.png");
            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }

        public static async Task<string> ConvertToBase64Async(Stream imageStream)
        {
            await using var memoryStream = new MemoryStream();
            await imageStream.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            return Convert.ToBase64String(imageBytes);
        }
    }
}
