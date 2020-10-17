using LuigiApp.Client.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LuigiApp.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewClientPage : ContentPage
    {
        public NewClientPage()
        {
            InitializeComponent();

            BindingContext = new NewClientViewModel();
        }
    }
}