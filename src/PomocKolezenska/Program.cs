using PomocKolezenska.Authorization.Requirements;
using PomocKolezenska.Components;
using PomocKolezenska.Data;
using PomocKolezenska.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using PomocKolezenska.Helpers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddRazorComponents()
    .AddInteractiveServerComponents();

services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source=PomocKolezenska.db"));

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/"; // TODO
        options.LoginPath = "/login";
        
        // Remove ReturnUrl parameter (for now)
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToAccessDenied = redirectContext =>
            {
                var uri = redirectContext.RedirectUri;
                UriHelper.FromAbsolute(uri, out var scheme, out var host, out var path, out var query, out var fragment);
                uri = UriHelper.BuildAbsolute(scheme, host, path);
                redirectContext.Response.Redirect(uri);
                return Task.CompletedTask;
            },
            OnRedirectToLogin = redirectContext =>
            {
                var uri = redirectContext.RedirectUri;
                UriHelper.FromAbsolute(uri, out var scheme, out var host, out var path, out var query, out var fragment);
                uri = UriHelper.BuildAbsolute(scheme, host, path);
                redirectContext.Response.Redirect(uri);
                return Task.CompletedTask;
            }
        };
    });

services.AddAuthorization(options =>
{
    options.AddPolicy("Anonymous", policy => policy.Requirements.Add(new AnonymousRequirement()));
});

services
    .AddSingleton<UserService>()
    .AddBlazorBootstrap()
    .AddControllers();


var app = builder.Build();
EnvironmentHelpers.AppInstance = app;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();