using LuigiApp.Client.Interactors;
using LuigiApp.Invoice.Models;
using LuigiApp.Base.ViewModels;
using System;
using System.Collections.ObjectModel;
using ClientModels = LuigiApp.Client.Models;
using Xamarin.Forms;
using LuigiApp.Resources;

namespace LuigiApp.Invoice.ViewModels
{
    [QueryProperty(nameof(obj), nameof(obj))]
    public class InvoiceDetailViewModel: BaseViewModel
    {
        public string obj
        {
            set
            {
                SetParameter(ref _invoice, value);
                LoadInvoiceProduct();
            }
        }
        private Models.Invoice _invoice;
        private ClientModels.Client client;
        private double total;
        private DateTime date;
        public ClientModels.Client Client
        {
            get => client;
            set => SetProperty(ref client, value);
        }
        public double Total
        {
            get => total;
            set =>SetProperty(ref total, value);
        }
        public DateTime Date
        {
            get => date; 
            set => SetProperty(ref date, value);
        }
        public ObservableCollection<InvoiceProduct> InvoiceProducts { get; }
        public InvoiceDetailViewModel()
        {
            Title = Literals.Invoirce;

            InvoiceProducts = new ObservableCollection<InvoiceProduct>();
        }
        private async void LoadInvoiceProduct()
        {
            Date = _invoice.Date;
            Total = _invoice.Total;
            Client = await ClientInteractor.Get(_invoice.ClientId);

            var invoiceProducts = await InvoiceProductInteractor.GetByInvoice(_invoice.Id);
            foreach (var invoiceProduct in invoiceProducts)
            {
                InvoiceProducts.Add(invoiceProduct);
            }
        }
    }
}
