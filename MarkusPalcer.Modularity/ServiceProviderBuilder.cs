using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MarkusPalcer.Modularity;

public class ServiceProviderBuilder
{
    private List<Assembly> _assemblies = new();
    
    public ServiceProviderBuilder WithAssembly(Assembly assembly)
    {
        _assemblies.Add(assembly);
        return this;
    }

    public ServiceProviderBuilder WithAssemblyOf<T>()
    {
        return WithAssembly(typeof(T).Assembly);
    }

    public IServiceProvider Build()
    {
        var assemblies = _assemblies.ToImmutableArray();

        var serviceCollection = new ServiceCollection();
        serviceCollection.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(cls => cls.AssignableTo<IModule>()).As<IModule>()
            .WithSingletonLifetime());
        var modules = serviceCollection.BuildServiceProvider().GetRequiredService<IEnumerable<IModule>>().ToArray();


        serviceCollection = new ServiceCollection();
        foreach (var module in modules)
        {
            module.RegisterTypes(serviceCollection, assemblies);
        }

        return serviceCollection.BuildServiceProvider();
    }
}