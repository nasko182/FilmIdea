namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using AspNetCore.Builder;
using AspNetCore.Identity;

using FilmIdea.Data.Models;

using static FilmIdea.Common.GeneralApplicationConstants;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// This method registers all services with their interfaces and implementations of given assembly
    /// </summary>
    /// <param name="serviceType"></param>Type of random service implementation
    /// <exception cref="InvalidOperationException"></exception>
    /// 
    public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
    {
        Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);

        if (serviceAssembly == null)
        {
            throw new InvalidOperationException("Invalid service type provided");
        }

        var serviceTypes = serviceAssembly.GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
            .ToArray();

        foreach (var sType in serviceTypes)
        {
            var interfaceType = sType
                .GetInterface($"I{sType.Name}");
            if (interfaceType == null)
            {
                throw new InvalidOperationException($"No interface is provided for the service with name {sType.Name}");
            }
            services.AddScoped(interfaceType, sType);
        }
    }

    public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();

        var serviceProvider = scopedServices.ServiceProvider;

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        Task.Run(async () =>
        {
            if (await roleManager.RoleExistsAsync(AdminRoleName))
            {
                return;
            }

            var role = new IdentityRole<Guid>(AdminRoleName);

            await roleManager.CreateAsync(role);

            try
            {
                var adminUser = await userManager.FindByEmailAsync(email);
                await userManager.AddToRoleAsync(adminUser, AdminRoleName);
            }
            catch
            {
                throw new Exception("User not found");
            }

        })
            .GetAwaiter()
            .GetResult();

        return app;
    }
}
