using JetBrains.Annotations;

namespace MarkusPalcer.Modularity;

public interface IFactory {}

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface IFactory<in TIn, out T> : IFactory
{
    T Create(TIn param);
}