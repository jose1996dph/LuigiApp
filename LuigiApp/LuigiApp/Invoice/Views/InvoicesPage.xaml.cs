using LuigiApp.Invoice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LuigiApp.Invoice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvoicesPage : ContentPage
    {
        InvoicesViewModel viewModel;
        public InvoicesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new InvoicesViewModel();
        }

        protected override void OnAppearing()
        {
            DateTime.Now.ToString("dd/MM/yyyy");
            DateTime.Now.ToString("dd/MM/yyyyy");
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}