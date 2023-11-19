using JetBrains.Annotations;
using MarkusPalcer.Modularity;

namespace MarkusPalcer.FileSystem.Module;

[UsedImplicitly]
public static class Extensions
{
    [UsedImplicitly]
    public static ServiceProviderBuilder WithFileSystemAbstractions(this ServiceProviderBuilder self)
    {
        return self.WithAssemblyOf<Module>();
    }
}