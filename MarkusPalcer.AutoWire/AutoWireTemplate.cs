using System.Windows.Markup;
using JetBrains.Annotations;

namespace MarkusPalcer.AutoWire;

[UsedImplicitly]
public class AutoWireTemplate : MarkupExtension
{
    private static readonly Lazy<DataTemplateSelector> Value = new(() => new DataTemplateSelector());
    
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value.Value;
    }
}