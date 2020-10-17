using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using Navigation = LuigiApp.Services.Navigation;
using Newtonsoft.Json;
using LuigiApp.Client.Interactors;
using LuigiApp.Product.Interactors;
using LuigiApp.Invoice.Interactors;

namespace LuigiApp.Base.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected Page RootPage => Application.Current.MainPage;
        protected Navigation.INavigation Navigation => DependencyService.Get<Navigation.INavigation>();
        protected IClientInteractor ClientInteractor => DependencyService.Get<IClientInteractor>();
        protected IProductInteractor ProductInteractor => DependencyService.Get<IProductInteractor>();
        protected IInvoiceInteractor InvoiceInteractor => DependencyService.Get<IInvoiceInteractor>();
        protected IInvoiceProductInteractor InvoiceProductInteractor => DependencyService.Get<IInvoiceProductInteractor>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected virtual void SetParameter<T>(ref T obj, string json)
        {
            obj = JsonConvert.DeserializeObject<T>(Uri.UnescapeDataString(json));
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
