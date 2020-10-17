using LuigiApp.Client.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LuigiApp.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientsPage : ContentPage
    {
        ClientsViewModel viewModel;
        public ClientsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ClientsViewModel();
        }
        protected override void OnAppearing()
        {
            viewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}