using FluentAssertions;

namespace MarkusPalcer.Extensions.Test;

[TestClass]
public class TypeExtensionsTests
{
    private class Type1;

    private class Type2 : Type1;

    private class Type3 : Type2;

    [TestMethod]
    public void TraverseBaseTypes_Traverses_in_correct_order()
    {
        typeof(Type3).TraverseBaseTypes().Should()
                     .ContainInConsecutiveOrder(typeof(Type3), typeof(Type2), typeof(Type1), typeof(object));
    }
}