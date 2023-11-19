using System.Collections.Immutable;
using System.Reflection;
using MarkusPalcer.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MarkusPalcer.FileSystem.Module;

public class Module : IModule
{
    public void RegisterTypes(IServiceCollection serviceCollection, ImmutableArray<Assembly> assemblies)
    {
        serviceCollection.AddSingleton<IFileSystem, Implementation.FileSystem>();
    }
}