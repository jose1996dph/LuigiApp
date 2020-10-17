using LuigiApp.Base.Views;
using LuigiApp.Client.Views;
using LuigiApp.Invoice.Views;
using LuigiApp.Product.Views;
using Xamarin.Forms;

namespace LuigiApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(InvoiceDetailPage), typeof(InvoiceDetailPage));
            Routing.RegisterRoute(nameof(NewInvoicePage), typeof(NewInvoicePage));
            Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));

            Routing.RegisterRoute(nameof(NewProductPage), typeof(NewProductPage));
            Routing.RegisterRoute(nameof(NewClientPage), typeof(NewClientPage));

            Routing.RegisterRoute(nameof(ScanPage), typeof(ScanPage));
        }
    }
}
