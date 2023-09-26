using JetBrains.Annotations;
using MarkusPalcer.Modularity;

namespace MarkusPalcer.AutoWire.Module;

[UsedImplicitly]
public static class Extensions
{
    [UsedImplicitly]
    public static ServiceProviderBuilder WithAutoWire(this ServiceProviderBuilder self)
    {
        return self.WithAssemblyOf<Module>();
    }
}