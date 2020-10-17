using LuigiApp.Invoice.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LuigiApp.Invoice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvoiceDetailPage : ContentPage
    {
        public InvoiceDetailPage()
        {
            InitializeComponent();

            BindingContext = new InvoiceDetailViewModel();
        }
    }
}