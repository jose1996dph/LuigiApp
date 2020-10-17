using LuigiApp.Product.Interactors;
using LuigiApp.Services.DataBase;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LuigiApp.Base.Views.Converters
{
    public class IdToDescriptionConverter<T> : IValueConverter where T : new()
    {
        protected IDataStore DataStore => DependencyService.Get<IDataStore>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)value;

            var task = Task.Run( async () =>
            {
                return await DataStore.Get<T>(id);
            });

            return new TaskCompletionNotifier<T>(task);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
