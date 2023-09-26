using System.Windows;

namespace MarkusPalcer.AutoWire;

public class AutoWire
{
    internal static IServiceProvider? ServiceProvider { get; set; }

    public static readonly DependencyProperty ViewModelTypeProperty = DependencyProperty.RegisterAttached(
        "ViewModelType", typeof(Type), typeof(AutoWire), new PropertyMetadata(default(Type), OnViewModelTypeChanged));

    private static void OnViewModelTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement uiElement)
        {
            return;
        }

        if (e.NewValue is not Type dataContextType)
        {
            uiElement.DataContext = null;
            return;
        }

        uiElement.DataContext = ServiceProvider?.GetService(dataContextType);
    }

    public static void SetViewModelType(DependencyObject element, Type value)
    {
        element.SetValue(ViewModelTypeProperty, value);
    }

    public static Type GetViewModelType(DependencyObject element)
    {
        return (Type)element.GetValue(ViewModelTypeProperty);
    }
}