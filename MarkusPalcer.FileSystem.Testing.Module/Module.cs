using System.Collections.Immutable;
using System.Reflection;
using MarkusPalcer.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MarkusPalcer.FileSystem.Testing.Module;

public class Module : IModule
{
    public void RegisterTypes(IServiceCollection serviceCollection, ImmutableArray<Assembly> assemblies)
    {
        serviceCollection.AddSingleton<FileSystem>();
        serviceCollection.Replace(ServiceDescriptor.Singleton<IFileSystem>(x => x.GetRequiredService<FileSystem>()));
    }
}