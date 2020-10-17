using LuigiApp.Base.ViewModels;
using LuigiApp.Client.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LuigiApp.Client.Interactors;
using LuigiApp.Client.Models;
using LuigiApp.Resources;

namespace LuigiApp.Client.ViewModels
{
    public class ClientsViewModel: BaseViewModel
    {
        public ObservableCollection<Models.Client> Clients { get; }
        public Command LoadClientsCommand { get; }
        public Command AddClientCommand { get; }
        public Command<Models.Client> ClientTapped { get; }

        public ClientsViewModel()
        {
            Title = Literals.Clients;
            Clients = new ObservableCollection<Models.Client>();
            LoadClientsCommand = new Command(async () => await ExecuteLoadClientsCommand());

            ClientTapped = new Command<Models.Client>(OnClientSelected);

            AddClientCommand = new Command(OnAddClient);
        }

        async Task ExecuteLoadClientsCommand()
        {
            IsBusy = true;

            try
            {
                this.Clients.Clear();
                var clients = await ClientInteractor.All();
                foreach (var client in clients)
                {
                    Clients.Add(client);
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

        private async void OnAddClient(object obj)
        {
            await Navigation.NavigateTo(nameof(NewClientPage), new Models.Client());
        }

        private async void OnClientSelected(Models.Client client)
        {
            await Navigation.NavigateTo(nameof(NewClientPage), client);
        }
    }
}
