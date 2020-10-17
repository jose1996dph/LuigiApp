using System;
using System.Globalization;
using Xamarin.Forms;

namespace LuigiApp.Base.Views.Converters
{
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (int)value;
            return number == 0 ? string.Empty : number.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (string)value;
            var result = 0;

            return int.TryParse(number.Replace(",","").Replace(".",""), out result) ? result : default(int);
        }
    }
}
