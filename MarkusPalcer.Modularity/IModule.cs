using System.Collections.Immutable;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace MarkusPalcer.Modularity;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface IModule
{
    /// <summary>
    /// Allows the module to register services with the <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="serviceCollection">The service collection to register services to</param>
    /// <param name="assemblies">
    /// The assemblies partaking in service registration. <br />
    /// You may scan all these for types you want to register, if your module auto-registers types with specific traits
    /// </param>
    void RegisterTypes(IServiceCollection serviceCollection, ImmutableArray<Assembly> assemblies);

    /// <summary>
    /// An optional callback that lets you modify the <see cref="IServiceCollection"/> after all modules have registered
    /// their services
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/> that will be used to create the <see cref="IServiceProvider"/></param>
    /// <param name="assemblies">The assemblies partaking in service registration.</param>
    void BeforeServiceProviderCreation(IServiceCollection serviceCollection, ImmutableArray<Assembly> assemblies)
    {
    }

    /// <summary>
    /// An optional callback that allows your module to work with the <see cref="IServiceProvider"/> once it has been created
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> that has been created</param>
    void AfterServiceProviderCreation(IServiceProvider serviceProvider)
    {
    }
}