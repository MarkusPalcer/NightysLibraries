using System.Windows;

namespace MarkusPalcer.AutoWire;

public class DataTemplateSelector : System.Windows.Controls.DataTemplateSelector
{
    private static Dictionary<Type, DataTemplate> _cache = new();

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is null) return null;
        
        if (_cache.TryGetValue(item.GetType(), out var result))
        {
            return result;
        }

        if (AutoWire.ServiceProvider is null)
        {
            return base.SelectTemplate(item, container);
        }

        var viewType = AutoWire.ServiceProvider.GetService(typeof(IViewFor<>).MakeGenericType(item.GetType()))?.GetType();
        if (viewType is null)
        {
            return base.SelectTemplate(item, container);
        }

        result = new DataTemplate
        {
            VisualTree = new FrameworkElementFactory(viewType)
        };

        _cache[item.GetType()] = result;

        return result;
    }
}