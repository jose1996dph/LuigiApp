using LuigiApp.Base.ViewModels;
using LuigiApp.Resources;
using System;
using System.Data;
using System.Diagnostics;
using Xamarin.Forms;

namespace LuigiApp.Client.ViewModels
{
    [QueryProperty(nameof(obj), nameof(obj))]
    public class NewClientViewModel: BaseViewModel
    {
        public string obj
        {
            set => SetParameter(ref client, value);
        }
        private Models.Client client;
        private string dni;
        private string name;
        public string Dni
        {
            get => dni;
            set => SetProperty(ref dni, value);
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public Command SaveClientCommand { get; }
        public NewClientViewModel()
        {
            Title = Literals.ClientNew;

            SaveClientCommand = new Command(OnSaveClient, ValidateClient);

            this.PropertyChanged +=
                (_, __) => SaveClientCommand.ChangeCanExecute();
        }
        protected override void SetParameter<T>(ref T obj, string json)
        {
            base.SetParameter(ref obj, json);
            Dni = client.Dni;
            Name = client.Name;
        }
        private bool ValidateClient()
        {
            return !String.IsNullOrWhiteSpace(Dni)
                && !String.IsNullOrWhiteSpace(Name);
        }
        private async void OnSaveClient()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;

            try
            {
                client.Set(Name, Dni);

                await ClientInteractor.Save(client);

                await Navigation.GoBack();
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
