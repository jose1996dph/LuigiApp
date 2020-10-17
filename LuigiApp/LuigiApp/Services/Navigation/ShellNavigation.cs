using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LuigiApp.Services.Navigation
{
    public class ShellNavigation : INavigation
    {
        public Task NavigateTo(string route)
        {
            return Shell.Current.GoToAsync(route);
        }

        public Task NavigateTo(string route, object obj)
        {
            var jsonObj = JsonConvert.SerializeObject(obj);
            return Shell.Current.GoToAsync($"{route}?{nameof(obj)}={jsonObj}");
        }

        public Task GoBack()
        {
            return Shell.Current.Navigation.PopAsync();
        }

        public Task GoBackModal()
        {
            return Shell.Current.Navigation.PopModalAsync();
        }
    }
}
