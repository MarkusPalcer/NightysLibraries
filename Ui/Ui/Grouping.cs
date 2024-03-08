using System.Windows;

namespace Ui;

public class Grouping
{
    public static readonly DependencyProperty PositionProperty = DependencyProperty.RegisterAttached(
        "Position",
        typeof(GroupPosition),
        typeof(Grouping),
        new PropertyMetadata(GroupPosition.Single));

    public static void SetPosition(DependencyObject element, GroupPosition value)
    {
        element.SetValue(PositionProperty, value);
    }

    public static GroupPosition GetPosition(DependencyObject element)
    {
        return (GroupPosition) element.GetValue(PositionProperty);
    }
}