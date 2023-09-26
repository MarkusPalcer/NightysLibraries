using System.Collections.Immutable;
using System.Reflection;
using MarkusPalcer.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MarkusPalcer.AutoWire.Module;

public class Module : IModule
{
    public void RegisterTypes(IServiceCollection serviceCollection, ImmutableArray<Assembly> assemblies)
    {
        serviceCollection.Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(y => y.AssignableTo(typeof(IViewFor<>))).AsImplementedInterfaces().WithTransientLifetime()
        );
    }

    public void AfterServiceProviderCreation(IServiceProvider serviceProvider)
    {
        serviceProvider.UseForAutoWirer();
    }
}