using LuigiApp.Product.ViewModels;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LuigiApp.Product.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewProductPage : ContentPage
    {
        NewProductViewModel viewModel;
        public NewProductPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new NewProductViewModel();
        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.Unsubscribe();
            return false;
        }
    }
}