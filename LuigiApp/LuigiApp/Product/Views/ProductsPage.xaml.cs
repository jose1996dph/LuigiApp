using LuigiApp.Product.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LuigiApp.Product.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        ProductsViewModel viewModel;
        public ProductsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ProductsViewModel();
        }

        protected override void OnAppearing()
        {
            viewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}