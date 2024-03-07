using System.Windows;

namespace Ui;

public class AttachedProperties
{
    public static readonly DependencyProperty GroupingProperty = DependencyProperty.RegisterAttached(
        "Grouping", typeof(Grouping), typeof(AttachedProperties), new FrameworkPropertyMetadata(Grouping.Single, FrameworkPropertyMetadataOptions.Inherits));

    public static void SetGrouping(DependencyObject element, Grouping value)
    {
        element.SetValue(GroupingProperty, value);
    }

    public static Grouping GetGrouping(DependencyObject element)
    {
        return (Grouping) element.GetValue(GroupingProperty);
    }
}