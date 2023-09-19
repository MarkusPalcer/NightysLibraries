using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MarkusPalcer.Modularity;

public interface IModule
{
    void RegisterTypes(IServiceCollection serviceCollection, ImmutableArray<Assembly> assemblies);
}