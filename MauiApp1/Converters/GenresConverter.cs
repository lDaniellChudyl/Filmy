using System.Globalization;
using MauiApp1.Controllers;
using MauiApp1.Models;

namespace MauiApp1.Converters;

public class GenresConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        List<GenreModel> values = value as List<GenreModel>;

        string output = string.Join(" / ", values.Select(r => r.name).ToArray());
        return output;
    }


    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}