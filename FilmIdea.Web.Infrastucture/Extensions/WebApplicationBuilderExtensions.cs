namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// This method registers all services with their interfaces and implementations of given assembly
    /// </summary>
    /// <param name="serviceType"></param>Type of random service implementation
    /// <exception cref="InvalidOperationException"></exception>
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
}
