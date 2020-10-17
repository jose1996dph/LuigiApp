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
    public partial class NewInvoicePage : ContentPage
    {
        NewInvoiceViewModel viewModel;
        public NewInvoicePage()
        {
            InitializeComponent();
            
            BindingContext = viewModel = new NewInvoiceViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            viewModel.UnSuscriber();
            return base.OnBackButtonPressed();
        }
    }
}