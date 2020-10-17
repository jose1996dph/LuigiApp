using LuigiApp.Base.ViewModels;
using LuigiApp.Product.Interactors;
using LuigiApp.Product.Models;
using LuigiApp.Product.Views;
using LuigiApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LuigiApp.Product.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Product> Products { get; }
        public Command LoadProductsCommand { get; }
        public Command AddProductCommand { get; }
        public Command<Models.Product> ProductTapped { get; }

        public ProductsViewModel()
        {
            Title = Literals.Products;
            Products = new ObservableCollection<Models.Product>();
            LoadProductsCommand = new Command(async () => await ExecuteLoadProductsCommand());

            ProductTapped = new Command<Models.Product>(OnProductSelected);

            AddProductCommand = new Command(OnAddProduct);

        }

        async Task ExecuteLoadProductsCommand()
        {
            IsBusy = true;

            try
            {
                this.Products.Clear();
                var products = await ProductInteractor.All();//await DataStore.GetProductsAsync(true);
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnAddProduct(object obj)
        {
            await Navigation.NavigateTo(nameof(NewProductPage), new Models.Product());
        }
        private async void OnProductSelected(Models.Product product)
        {
            await Navigation.NavigateTo(nameof(NewProductPage), product);
        }

    }
}
