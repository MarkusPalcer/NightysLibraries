using System.Collections.ObjectModel;

namespace MarkusPalcer.Extensions;

public static class Enumerable
{
    /// <summary>
    /// Converts the <see cref="IEnumerable{T}"/> to an observable collection
    /// </summary>
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> self) =>
        new ObservableCollection<T>(self);
}