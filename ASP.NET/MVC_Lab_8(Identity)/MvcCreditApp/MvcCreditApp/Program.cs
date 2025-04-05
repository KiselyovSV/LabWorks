using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcCreditApp1.Data;
using MvcCreditApp1.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MvcCreditApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false; // отключение подтверждения электронной почты
            options.SignIn.RequireConfirmedPhoneNumber = false; // отключение подтверждения номера телефона
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<CreditContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("CreditContext") ?? 
        throw new InvalidOperationException("Connection string'CreditContext' not found.")));
        // Добавьте выходное ПО промежуточного слоя кэширования в коллекцию служб путем вызова метода AddOutputCache():
        builder.Services.AddOutputCache();
        var app = builder.Build();

        // Данными БД заполняется при запуске приложения, поэтому в файл Program.cs добавьте следующий код:

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
        // Далее добавьте ПО промежуточного слоя в конвейер обработки запросов путем вызова UseOutputCache():
        app.UseOutputCache();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}
