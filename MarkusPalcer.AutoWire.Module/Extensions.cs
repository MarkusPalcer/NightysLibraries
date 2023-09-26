using MarkusPalcer.Modularity;

namespace MarkusPalcer.AutoWire.Module;

public static class Extensions
{
    public static ServiceProviderBuilder WithAutoWire(this ServiceProviderBuilder self)
    {
        return self.WithAssemblyOf<Module>();
    }
}