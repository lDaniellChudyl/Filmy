using System.Globalization;
using MauiApp1.Models;

namespace MauiApp1.Converters;

public class RatingConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double values = (double)value;

        string output = Math.Round(values, 1).ToString();
        return output;
    }


    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}