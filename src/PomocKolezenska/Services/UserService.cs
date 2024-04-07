using System.Security.Claims;
using PomocKolezenska.Data;
using PomocKolezenska.Models.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using PomocKolezenska.Extensions;
using PomocKolezenska.Helpers;
using Sodium;
using ClaimTypes = PomocKolezenska.Authorization.ClaimTypes;

namespace PomocKolezenska.Services;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _context = contextFactory.CreateDbContext();
    }

    public async Task<User> CreateAsync(string username, string email, string password)
    {
        if (await _context.Users.AnyAsync(user => user.Username == username))
        {
            throw new ArgumentException("This username is already in use.");
        }
        if (await _context.Users.AnyAsync(user => user.Email == email))
        {
            throw new ArgumentException("This email address is already in use.");
        }

        var user = new User()
        {
            Email = email,
            Username = username,
            UserImageBase64 = await ImageConversionHelpers.ConvertToBase64Async(ImageConversionHelpers.OpenDefaultProfilePicture()),
            HashedPassword = PasswordHash.ArgonHashString(password)
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }
    
    public async Task<User?> GetUser(ApplicationDbContext context, AuthenticationStateProvider authenticationStateProvider)
    {
        var state = await authenticationStateProvider.GetAuthenticationStateAsync();
        var userId = state.User.GetClaim(ClaimTypes.UserId);
        if (userId is null)
            return null;

        return await context.Users.FirstOrDefaultAsync(user => user.Id == Guid.Parse(userId));
    }

    public Task LogInAsync(User user, HttpContext httpContext)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.UserId, user.Id.ToString()),
            new(ClaimTypes.Username, user.Username),
            // new(ClaimTypes.UserImage, user.UserImageBase64)
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        return httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
    }

    public async Task LogInAsync(string usernameOrEmail, string password, HttpContext httpContext)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == usernameOrEmail 
                                                                    || user.Username == usernameOrEmail);
        if (user is null || !PasswordHash.ArgonHashStringVerify(user.HashedPassword, password))
            throw new ArgumentException("Incorrect credentials.");

        await LogInAsync(user, httpContext);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
