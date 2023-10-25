namespace MarkusPalcer.Modularity;

/// <summary>
/// Helper to apply configuration to modules
/// </summary>
public interface IModuleConfigurator
{
    /// <summary>
    /// Applies the configuration to the module
    /// </summary>
    void Configure(IModule module);
}

public class ModuleConfigurator<TConfig> : IModuleConfigurator
{
    private readonly Func<TConfig> _configurationFactory;

    public ModuleConfigurator(Func<TConfig> configurationFactory)
    {
        _configurationFactory = configurationFactory;
    }

    public void Configure(IModule module)
    {
        if (module is not IConfigurableModule<TConfig> configurableModule)
            throw new InvalidOperationException(
                $"Tried to configure a module of type {module.GetType().FullName} which does not implement {typeof(IConfigurableModule<TConfig>).FullName}");

        var config = _configurationFactory();

        configurableModule.Configure(config);
    }
}