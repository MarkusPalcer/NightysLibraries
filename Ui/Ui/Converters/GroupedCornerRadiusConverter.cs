using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ui.Converters;

public class GroupedCornerRadiusConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var baseValue = values[0] as CornerRadius? ??
                        throw new InvalidOperationException(
                            "First argument to GroupedCornerRadiusConverter must be the base corner radius to modify");
        if (values[1] == DependencyProperty.UnsetValue) values[1] = GroupPosition.Single;
        var grouping = values[1] as GroupPosition? ??
                       throw new InvalidOperationException(
                           "Second argument to GroupedCornerRadiusConverter must be the grouping to apply");


        return grouping switch
        {
            GroupPosition.Single => baseValue,
            GroupPosition.First => baseValue with { TopRight = 0, BottomRight = 0 },
            GroupPosition.Middle => new CornerRadius(0),
            GroupPosition.Last => baseValue with { TopLeft = 0, BottomLeft = 0 },
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