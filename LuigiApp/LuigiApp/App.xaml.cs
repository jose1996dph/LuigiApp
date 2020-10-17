using Xamarin.Forms;
using LuigiApp.Services.Navigation;
using LuigiApp.Services.DataBase;
using LuigiApp.Client.Interactors;
using LuigiApp.Product.Interactors;
using LuigiApp.Invoice.Interactors;
using System.Globalization;

namespace LuigiApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.RegisterSingleton(new SQLiteDataBase());
            DependencyService.RegisterSingleton(new ShellNavigation());
            DependencyService.RegisterSingleton(new ClientInteractor());
            DependencyService.RegisterSingleton(new ProductInteractor());
            DependencyService.RegisterSingleton(new InvoiceInteractor());
            DependencyService.RegisterSingleton(new InvoiceProductInteractor());
            
            MainPage = new AppShell();
        }
    }
}
