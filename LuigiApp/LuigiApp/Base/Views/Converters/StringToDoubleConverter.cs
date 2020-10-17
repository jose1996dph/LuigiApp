using System;
using System.Globalization;
using Xamarin.Forms;

namespace LuigiApp.Base.Views.Converters
{
    public class StringToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (double)value;
            return number == 0 ? string.Empty : number.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (string)value;
            var result = 0.0;
            //return string.IsNullOrWhiteSpace(number) ? default(double) : double.Parse(number, CultureInfo.CurrentCulture);
            return double.TryParse(number, out result) ? result : default(double);
        }
    }
}
