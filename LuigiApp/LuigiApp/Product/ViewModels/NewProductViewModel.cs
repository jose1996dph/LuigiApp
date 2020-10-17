using LuigiApp.Base.ViewModels;
using LuigiApp.Base.Views;
using LuigiApp.Product.Interactors;
using LuigiApp.Resources;
using System;
using System.Data;
using System.Diagnostics;
using Xamarin.Forms;

namespace LuigiApp.Product.ViewModels
{
    [QueryProperty(nameof(obj), nameof(obj))]
    public class NewProductViewModel : BaseViewModel
    {
        public string obj
        {
            set => SetParameter(ref product, value);
        }
        private Models.Product product;
        private string code;
        private string description;
        private double price;
        public string Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public double Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public Command UnsubscribeCommand { get; }
        public Command ScanProductCommand { get; }
        public Command SaveProductCommand { get; }
        public NewProductViewModel()
        {
            Title = Literals.ProductNew;

            SaveProductCommand = new Command(OnSaveProduct, ValidateProduct);
            UnsubscribeCommand = new Command(Unsubscribe);
            ScanProductCommand = new Command(GoScan);

            this.PropertyChanged +=
                (_, __) => SaveProductCommand.ChangeCanExecute();

            MessagingCenter.Subscribe<ScanPage, string>(this, Literals.Code, OnScanProduct);
        }

        public async void Unsubscribe()
        {
            try
            {
                MessagingCenter.Unsubscribe<ScanPage, string>(this, Literals.Code);
                await Navigation.NavigateTo("..");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        protected override void SetParameter<T>(ref T obj, string json)
        {
            base.SetParameter(ref obj, json);
            Code = product.Code;
            Description = product.Description;
            Price = product.Price;
        }

        private bool ValidateProduct()
        {
            return !String.IsNullOrWhiteSpace(Code)
                && !String.IsNullOrWhiteSpace(Description)
                && Price > 0;
        }
        private async void GoScan()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            await Navigation.NavigateTo(nameof(ScanPage));

            IsBusy = false;
        }
        private async void OnScanProduct(ScanPage page, string code)
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            Code = code;

            await Navigation.GoBack();

            IsBusy = false;
        }
        private async void OnSaveProduct()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                product.Set(Code, Description, Price);

                await ProductInteractor.Save(product);

                Unsubscribe();
            }
            catch (DuplicateNameException error)
            {
                await RootPage.DisplayAlert(Literals.Error, error.Message, Literals.Ok);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
