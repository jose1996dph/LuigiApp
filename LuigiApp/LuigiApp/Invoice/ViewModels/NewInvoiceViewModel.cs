using LuigiApp.Invoice.Models;
using LuigiApp.Invoice.Views;
using LuigiApp.Base.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using LuigiApp.Resources;

namespace LuigiApp.Invoice.ViewModels
{
    public class NewInvoiceViewModel: BaseViewModel
    {
        private string dni;
        private string name;
        private double total;

        public string Dni
        {
            get => dni;
            set
            {
                SetProperty(ref dni, value);
                SearchClient();
            }
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public double Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }
        public ObservableCollection<InvoiceProduct> InvoiceProducts { get; }
        public NewInvoiceViewModel()
        {
            Title = Literals.Invoirce;

            InvoiceProducts = new ObservableCollection<InvoiceProduct>();

            SaveCommand = new Command(OnSave, ValidateSave);
            AddProductCommand = new Command(OnAddProduct);
            InvoiceProductTapped = new Command<InvoiceProduct>(OnInvoiceProductTapped);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            MessagingCenter.Subscribe<AddProductViewModel, InvoiceProduct>(this, Literals.AddProduct, (_, invoiceProduct) =>
            {
                InvoiceProducts.Add(invoiceProduct);
                Total += invoiceProduct.Total;
            });
        }
        public void UnSuscriber()
        {
            MessagingCenter.Unsubscribe<AddProductViewModel, InvoiceProduct>(this, Literals.AddProduct);
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Dni)
                && !String.IsNullOrWhiteSpace(Name)
                && InvoiceProducts.Count > 0;
        }
        private async void SearchClient()
        {
            var client = await ClientInteractor.Get(Dni);
            Name = client?.Name;
        }
        public Command SaveCommand { get; }
        private async void OnSave()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            if (!await RootPage.DisplayAlert(Literals.Atention, Literals.Finish, Literals.Yes, Literals.No))
            {
                return;
            }

            try
            {
                var client = await ClientInteractor.Get(Dni);
                if (client == null)
                {
                    client = new Client.Models.Client(Name, Dni);
                }
                else
                {
                    client.Name = Name;
                }
                
                await ClientInteractor.Save(client);

                var invoice = new Models.Invoice(client.Id, InvoiceProducts.ToList(), Total);

                await InvoiceInteractor.Save(invoice);
                foreach (var invoiceProduct in InvoiceProducts)
                {
                    invoiceProduct.InvoiceId = invoice.Id;
                    await InvoiceProductInteractor.Save(invoiceProduct);
                }

                await Navigation.GoBack();
                UnSuscriber();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            IsBusy = false;
        }
        public Command AddProductCommand { get; }
        private async void OnAddProduct()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            await Navigation.NavigateTo(nameof(AddProductPage));

            IsBusy = false;
        }
        public Command<InvoiceProduct> InvoiceProductTapped { get; }
        private async void OnInvoiceProductTapped(InvoiceProduct invoiceProduct)
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            if  (await RootPage.DisplayAlert(Literals.Atention, Literals.DeleteRegister, Literals.Yes, Literals.No))
            {
                InvoiceProducts.Remove(invoiceProduct);
                Total -= invoiceProduct.Total;
            }

            IsBusy = false;
        }
    }
}
