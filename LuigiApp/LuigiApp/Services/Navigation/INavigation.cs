using System.Threading.Tasks;

namespace LuigiApp.Services.Navigation
{
    public interface INavigation
    {
        Task NavigateTo(string route);
        Task NavigateTo(string route, object obj);
        Task GoBack();
        Task GoBackModal();
    }
}
