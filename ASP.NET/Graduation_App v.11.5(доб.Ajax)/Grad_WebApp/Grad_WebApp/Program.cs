using Grad_WebApp.Data;
using Grad_WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Grad_WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(connectionString));

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(connectionString));
            builder.Services.AddDbContext<FitnessDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("FitnessDbContext") ?? throw new InvalidOperationException("Connection string 'FitnessDbContext' not found.")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // ��������� FitnessDbContext 

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders(); // ��������� ���������������� ��������� �������
            builder.Services.AddRazorPages();
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  // ����� �������������� - � ������� jwt-������� (���������� �������� � ������ ����� Nuget ����� Microsoft.AspNetCore.Authentication.JwtBearer)
            //                .AddJwtBearer();      // ����������� �������������� � ������� jwt-�������
            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)  // ����� �������������� - � ������� ����
            //                .AddCookie();      // ����������� � �������������� �������������� � ������� ����
            builder.Services.AddAuthorization();            // ���������� �������� �����������
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<EmailService>(); // ��������� ������ EmailService (����� �����, ������������� Nuget-����� MailKit)
            builder.Services.AddSingleton<Id>(); // ��������� ������ Id
            builder.Services.AddOutputCache(options => // ��������� ������ ����
            {
                options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromMinutes(0)).Tag("ttbls")); // �������� ���� �� ���������
                options.AddPolicy("Expire2", builder => builder.Expire(TimeSpan.FromMinutes(20)).Tag("ttbls2"));        // �������� ���� "Expire2"
                options.AddPolicy("Expire3", builder => builder.Expire(TimeSpan.FromMinutes(20)).Tag("ttbls3"));   // �������� ���� "Expire3"
                options.AddPolicy("Expire4", builder => builder.Expire(TimeSpan.FromMinutes(20)).Tag("ttbls4"));        // �������� ���� "Expire4"
                options.AddPolicy("Expire5", builder => builder.Expire(TimeSpan.FromMinutes(20)).Tag("ttbls5"));   // �������� ���� "Expire5"
                options.AddPolicy("Expire6", builder => builder.Expire(TimeSpan.FromMinutes(20)).Tag("ttbls6"));        // �������� ���� "Expire6"
                options.AddPolicy("Expire7", builder => builder.Expire(TimeSpan.FromMinutes(20)).Tag("ttbls7"));   // �������� ���� "Expire7"
                options.AddPolicy("Expire8", builder => builder.Expire(TimeSpan.FromMinutes(20)).Tag("ttbls8"));        // �������� ���� "Expire8"
            }

                ); // ��������� ������� ����
            var app = builder.Build();
            app.UseAuthentication();// ���������� middleware ��������������
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
            app.UseOutputCache(); // ��������� OutputCacheMiddleware (������ � ��� �����, � ������ �������� �� �����!)
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Timetables}/{action=Index}/{id?}");
            app.MapRazorPages();
            app.Run();
        }
    }
}
