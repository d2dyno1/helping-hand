using dotnetBB.Authorization.Requirements;
using dotnetBB.Components;
using dotnetBB.Data;
using dotnetBB.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddRazorComponents()
    .AddInteractiveServerComponents();

services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source=dotnetBB.db"));

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
    .AddControllers();


var app = builder.Build();

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