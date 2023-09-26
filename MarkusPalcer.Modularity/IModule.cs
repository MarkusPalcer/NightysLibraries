using System.Collections.Immutable;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace MarkusPalcer.Modularity;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface IModule
{
    void RegisterTypes(IServiceCollection serviceCollection, ImmutableArray<Assembly> assemblies);
    
    void AfterServiceProviderCreation(IServiceProvider serviceProvider) {}
}