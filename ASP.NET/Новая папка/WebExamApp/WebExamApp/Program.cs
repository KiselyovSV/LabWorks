using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebExamApp.Data;
using WebExamApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebExamApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
        builder.Services.AddDbContext<StatementDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("StatementDbContext") ?? throw new InvalidOperationException("Connection string'StatementDbContext' not found.")));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false; // отключение подтверждения электронной почты
            options.SignIn.RequireConfirmedPhoneNumber = false; // отключение подтверждения номера телефона
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            SeedData.Initialize(services);
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Statement}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
