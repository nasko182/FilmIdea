namespace FilmIdea.Web;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Services.Data.Interfaces;
using Data;
using Data.Models;

using static Common.GeneralApplicationConstants;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

    
        var connectionString = 
            builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services
            .AddDbContext<FilmIdeaDbContext>(options =>
            options.UseSqlServer(connectionString)); 

        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                // Sign In
                options.SignIn.RequireConfirmedAccount = builder
                    .Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");

                //Password
                options.Password.RequireDigit = builder
                    .Configuration.GetValue<bool>("Identity:Password:RequireDigit");
                options.Password.RequireLowercase = builder
                    .Configuration.GetValue<bool>("Identity:SignIn:RequireLowercase");
                options.Password.RequireUppercase = builder
                    .Configuration.GetValue<bool>("Identity:SignIn:RequireUppercase");
                options.Password.RequireNonAlphanumeric = builder
                    .Configuration.GetValue<bool>("Identity:SignIn:RequireNonAlphanumeric");
                options.Password.RequiredLength = builder
                    .Configuration.GetValue<int>("Identity:Password:RequiredLength");

                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>() 
            .AddEntityFrameworkStores<FilmIdeaDbContext>();

        builder.Services.AddApplicationServices(typeof(IMovieService));

        builder.Services.AddRecaptchaService();

        builder.Services.AddMemoryCache();

        builder.Services.ConfigureApplicationCookie(cfg =>
        {
            cfg.LoginPath = "/User/Login";
            cfg.AccessDeniedPath = "/Home/Error/401";
        });

        builder.Services
            .AddControllersWithViews()
            .AddMvcOptions(option =>
            {
                option.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error/500");
            app.UseStatusCodePagesWithRedirects("Home/Error?statusCode={0}");

            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.EnableOnlineUsersCheck();

        if (app.Environment.IsDevelopment())
        {
            app.SeedAdministrator(DevelopmentAdminEmail);
        }

        app.UseEndpoints(config =>
        {
            config.MapControllerRoute(
                    name: "areas",
                    pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            //config.MapControllerRoute(
            //    name: "ProtectingUrlRoute",
            //    pattern: "/{controller}/{action}/{id}/{information}",
            //    defaults: new { Controller = "Movie", Action = "Details" });

            config.MapDefaultControllerRoute();

            config.MapRazorPages();
        });

        app.Run();
    }
}