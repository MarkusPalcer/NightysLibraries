using System.Windows;
using MarkusPalcer.Extensions;

namespace MarkusPalcer.AutoWire;

public class DataTemplateSelector : System.Windows.Controls.DataTemplateSelector
{
    private static readonly Dictionary<Type, DataTemplate> Cache = new();

    internal DataTemplateSelector()
    {
    }

    public override DataTemplate SelectTemplate(object? item, DependencyObject container)
    {
        if (item is null) return null!;

        if (Cache.TryGetValue(item.GetType(), out var result))
        {
            return result;
        }

        if (AutoWire.ServiceProvider is null)
        {
            return base.SelectTemplate(item, container)!;
        }

        var viewType = item.GetType()
            .TraverseBaseTypes()
            .Concat(item.GetType().GetInterfaces())
            .Select(type => AutoWire.ServiceProvider.GetService(typeof(IViewFor<>).MakeGenericType(type))?.GetType())
            .FirstOrDefault(x => x is not null);

        if (viewType is null)
        {
            return base.SelectTemplate(item, container)!;
        }

        result = new DataTemplate
        {
            VisualTree = new FrameworkElementFactory(viewType)
        };

        Cache[item.GetType()] = result;

        return result;
    }
}