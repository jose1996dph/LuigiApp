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
    public partial class AddProductPage : ContentPage
    {
        AddProductViewModel viewModel;
        public AddProductPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AddProductViewModel();
        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.Unsubscribe();
            return false;
        }
        private void OnProductSelected(object sender, EventArgs e)
        {
            var elemnt = sender as Label;
            if (elemnt == null)
            {
                return;
            }
            viewModel.SelectProductCommand.Execute(elemnt.BindingContext as Product.Models.Product);
        }

        private void ValidateInt(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Contains("-"))
            {
                var elemnt = sender as Entry;
                elemnt.Text = e.OldTextValue;
            }
        }
    }
}