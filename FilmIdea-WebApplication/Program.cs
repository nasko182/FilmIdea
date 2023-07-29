namespace FilmIdea.Web;

using Microsoft.EntityFrameworkCore;

using Data;
using Data.Models;
using Services.Data.Interfaces;

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
            })
            .AddEntityFrameworkStores<FilmIdeaDbContext>();

        builder.Services.AddApplicationServices(typeof(IMovieService));

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapDefaultControllerRoute();
        app.MapRazorPages();

        app.Run();

        //TODO: Remove review and comment seed after review logic is implemented
    }
}