using BasiliskDataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BasiliskWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Dependencies.ConfigureService(builder.Configuration, builder.Services);

        IServiceCollection services = builder.Services;
        services.AddControllersWithViews();
        services.AddBusinessServices();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "AccountCookie";
            options.LoginPath = "/Login";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.AccessDeniedPath = "/AccessDenied";
        });
        services.AddAuthorization();

        var app = builder.Build();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Index}"
        );

        app.UseStaticFiles();
        app.Run();
    }
}
