using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MarkusPalcer.Modularity;

public class Module : IModule
{
    public void RegisterTypes(IServiceCollection serviceCollection, ImmutableArray<Assembly> assemblies)
    {
        serviceCollection.Scan(scan => scan.FromAssemblies(assemblies)
            // TODO: Only register as IFactory<...>
            .AddClasses(c => c.AssignableTo(typeof(IFactory))).AsImplementedInterfaces().WithSingletonLifetime()
        );
    }
}