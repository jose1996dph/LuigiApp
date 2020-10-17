using LuigiApp.Invoice.Views;
using LuigiApp.Base.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using LuigiApp.Resources;

namespace LuigiApp.Invoice.ViewModels
{
    public class InvoicesViewModel: BaseViewModel
    {
        private Models.Invoice _selectedInvoice;

        public ObservableCollection<Models.Invoice> Invoices { get; }
        public Command LoadInvoicesCommand { get; }
        public Command AddInvoiceCommand { get; }
        public Command<Models.Invoice> InvoiceTapped { get; }

        public InvoicesViewModel()
        {
            Title = Literals.Invoirce;

            Invoices = new ObservableCollection<Models.Invoice>();
            LoadInvoicesCommand = new Command(async () => await ExecuteLoadInvoicesCommand());

            InvoiceTapped = new Command<Models.Invoice>(OnInvoiceSelected);

            AddInvoiceCommand = new Command(OnAddInvoice);
        }

        async Task ExecuteLoadInvoicesCommand()
        {
            IsBusy = true;

            try
            {
                this.Invoices.Clear();
                var invoices = await InvoiceInteractor.All();//await DataStore.GetInvoicesAsync(true);
                foreach (var invoice in invoices)
                {
                    Invoices.Add(invoice);
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
            SelectedInvoice = null;
        }

        public Models.Invoice SelectedInvoice
        {
            get => _selectedInvoice;
            set
            {
                SetProperty(ref _selectedInvoice, value);
                OnInvoiceSelected(value);
            }
        }

        private async void OnAddInvoice(object obj)
        {
            await Navigation.NavigateTo(nameof(NewInvoicePage));
        }

        async void OnInvoiceSelected(Models.Invoice invoice)
        {
            if (invoice == null)
                return;

            // This will push the InvoiceDetailPage onto the navigation stack
            await Navigation.NavigateTo(nameof(InvoiceDetailPage), invoice);
            //await Shell.Current.GoToAsync($"{nameof(InvoiceDetailPage)}?{nameof(InvoiceDetailViewModel.InvoiceId)}={Invoice.Id}");
        }
    }
}
