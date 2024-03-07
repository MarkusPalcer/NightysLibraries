using System.Windows;
using System.Windows.Media;

namespace Ui;

public class AttachedProperties
{
    public static readonly DependencyProperty InvertedForegroundProperty = DependencyProperty.RegisterAttached(
        "InvertedForeground",
        typeof(Brush),
        typeof(AttachedProperties),
        new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.Inherits));

    public static void SetInvertedForeground(DependencyObject element, Brush value)
    {
        element.SetValue(InvertedForegroundProperty, value);
    }

    public static Brush GetInvertedForeground(DependencyObject element)
    {
        return (Brush)element.GetValue(InvertedForegroundProperty);
    }
}