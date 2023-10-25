using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MarkusPalcer.Modularity;

public class ServiceProviderBuilder
{
    private readonly List<Assembly> _assemblies = new() {typeof(ServiceProviderBuilder).Assembly};

    private readonly Dictionary<Type, IModuleConfigurator> _configurators = new();


    /// <summary>
    /// Adds the given assembly to the module and type discovery
    /// </summary>
    /// <param name="assembly">The assembly to add</param>
    public ServiceProviderBuilder WithAssembly(Assembly assembly)
    {
        _assemblies.Add(assembly);
        return this;
    }

    /// <summary>
    /// Adds the assembly of the given type to the module and type discovery
    /// </summary>
    /// <typeparam name="T">The type to get the assembly from</typeparam>
    public ServiceProviderBuilder WithAssemblyOf<T>()
    {
        return WithAssembly(typeof(T).Assembly);
    }

    /// <summary>
    /// Configures the given module
    /// </summary>
    /// <param name="configurationFactory">A factory function creating the configuration for the module</param>
    /// <typeparam name="TModule">The concrete type of the module to configure</typeparam>
    /// <typeparam name="TConfig">The type of the configuration for the module</typeparam>
    public ServiceProviderBuilder Configure<TModule, TConfig>(Func<TConfig> configurationFactory)
        where TModule : IConfigurableModule<TConfig>
    {
        _configurators[typeof(TModule)] = new ModuleConfigurator<TConfig>(configurationFactory);
        return this;
    }

    /// <summary>
    /// Configures the given module
    /// </summary>
    /// <param name="configurator">A method that sets up the configuration for the module</param>
    /// <typeparam name="TModule">The concrete type of the module to configure</typeparam>
    /// <typeparam name="TConfig">The type of the configuration for the module</typeparam>
    public ServiceProviderBuilder Configure<TModule, TConfig>(Action<TConfig> configurator) where TConfig : new() where TModule : IConfigurableModule<TConfig>
    {
        _configurators[typeof(TModule)] = new ModuleConfigurator<TConfig>(() =>
        {
            var config = new TConfig();
            configurator(config);
            return config;
        });
        return this;
    }

    /// <summary>
    /// Executes module discovery, lets modules populate the <see cref="ServiceCollection"/> and then builds the <see cref="ServiceProvider"/>
    /// </summary>
    /// <returns>The constructed <see cref="ServiceProvider"/></returns>
    public IServiceProvider Build()
    {
        var assemblies = _assemblies.ToImmutableArray();

        var serviceCollection = new ServiceCollection();
        serviceCollection.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(cls => cls.AssignableTo<IModule>()).As<IModule>()
            .WithSingletonLifetime());
        var modules = serviceCollection.BuildServiceProvider().GetRequiredService<IEnumerable<IModule>>().ToArray();


        foreach (var module in modules)
        {
            if (!_configurators.TryGetValue(module.GetType(), out var configurator)) continue;
            configurator.Configure(module);
        }

        serviceCollection = new ServiceCollection();
        foreach (var module in modules)
        {
            module.RegisterTypes(serviceCollection, assemblies);
        }

        foreach (var module in modules)
        {
            module.BeforeServiceProviderCreation(serviceCollection, assemblies);
        }

        var result = serviceCollection.BuildServiceProvider();

        foreach (var module in modules)
        {
            module.AfterServiceProviderCreation(result);
        }

        return result;
    }
}