namespace MarkusPalcer.Modularity;

public interface IConfigurableModule<in TConfig> : IModule
{
    void Configure(TConfig config);
}