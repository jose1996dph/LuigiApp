using LuigiApp.Base.ViewModels;
using Model = LuigiApp.Product.Models;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using LuigiApp.Invoice.Models;
using LuigiApp.Product.Interactors;
using System.Diagnostics.Contracts;
using LuigiApp.Base.Views;
using System.Diagnostics;
using LuigiApp.Resources;

namespace LuigiApp.Invoice.ViewModels
{
    public class AddProductViewModel : BaseViewModel
    {
        private IProductInteractor productRepository;
        private Model.Product product;
        private string query;
        private double total;
        private double price;
        private double quantity;

        public string Query
        {
            get => query;
            set
            {
                SetProperty(ref query, value);
                OnSearchProduct();
            }
        }
        public Model.Product Product
        {
            get => product;
            set
            {
                SetProperty(ref product, value);
                Price = Product?.Price ?? 0;
            }
        }
        public double Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }
        public double Price
        {
            get => price;
            set
            {
                SetProperty(ref price, value);
                Total = Price * Quantity;
            }
        }
        public double Quantity
        {
            get => quantity;
            set
            {
                SetProperty(ref quantity, value);
                Total = Price * (double)Quantity;
            }
        }

        public Command ScanCommand { get; }
        public Command SearchCommand { get; }
        public Command AddProductCommand { get; }
        public Command UnsubscribeCommand { get; }
        public Command<Model.Product> SelectProductCommand { get; }
        public ObservableCollection<Model.Product> Products { get; }
        public AddProductViewModel()
        {
            Title = Literals.Product;

            Products = new ObservableCollection<Model.Product>();
            
            ScanCommand = new Command(GoScan);
            SearchCommand = new Command(OnSearchProduct);
            AddProductCommand = new Command(AddProduct, ValidateAdd);
            UnsubscribeCommand = new Command(Unsubscribe);
            SelectProductCommand = new Command<Model.Product>(OnSelectProduct);

            this.PropertyChanged += (_, __) => AddProductCommand.ChangeCanExecute();

            productRepository = new ProductInteractor();

            MessagingCenter.Subscribe<ScanPage, string>(this, Literals.Code, OnScanProduct);
        }
        public async void Unsubscribe()
        {
            try
            {
                MessagingCenter.Unsubscribe<ScanPage, string>(this, Literals.Code);
                await Navigation.NavigateTo("..");
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private bool ValidateAdd()
        {
            return Product != null
                && Quantity > 0;
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

            try
            {
                Product = await productRepository.Get(code);
                Query = Product?.Code;

                await Navigation.GoBack();
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async void OnSearchProduct()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            
            Products.Clear();

            var products = await productRepository.Search(Query);

            foreach (var product in products)
            {
                Products.Add(product);
            }

            IsBusy = false;
        }
        private void AddProduct()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            var invoiceProduct = new InvoiceProduct(Quantity, Total, Product.Price, Product.Id);
            MessagingCenter.Send(this, Literals.AddProduct, invoiceProduct);
            Unsubscribe();

            IsBusy = false;
        }
        private void OnSelectProduct(Model.Product product)
        {
            if (product == null)
            {
                return;
            }

            Query = product.Description;
            Product = product;
        }
    }
}
