using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ui.Converters;

public class GroupedCornerRadiusConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var baseValue = values[0] as CornerRadius? ?? throw new InvalidOperationException("First argument to GroupedCornerRadiusConverter must be the base corner radius to modify");
        if (values[1] == DependencyProperty.UnsetValue) values[1] = Grouping.Single;
        var grouping = values[1] as Grouping? ?? throw new InvalidOperationException("Second argument to GroupedCornerRadiusConverter must be the grouping to apply");

        return grouping switch
        {
            Grouping.Single => baseValue,
            Grouping.First => new CornerRadius(baseValue.TopLeft, 0, 0, baseValue.BottomLeft),
            Grouping.Middle => new CornerRadius(0),
            Grouping.Last => new CornerRadius(0, baseValue.TopRight, baseValue.BottomRight, 0),
#pragma warning disable CA2208
            _ => throw new ArgumentOutOfRangeException(nameof(grouping))
#pragma warning restore CA2208
        };
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}