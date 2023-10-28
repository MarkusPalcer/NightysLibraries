namespace MarkusPalcer.Extensions;

public static class TypeExtensions
{
    /// <summary>
    /// Goes up the type tree, navigation to the base type until it reaches the type root
    /// </summary>
    /// <returns>An enumerable of all types in the given type's hierarchy</returns>
    public static IEnumerable<Type> TraverseBaseTypes(this Type self)
    {
        var currentType = self;
        while (currentType != null)
        {
            yield return currentType;
            currentType = currentType.BaseType;
        }
    }


}