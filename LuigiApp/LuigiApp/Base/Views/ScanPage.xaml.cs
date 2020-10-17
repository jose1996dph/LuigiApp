using LuigiApp.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LuigiApp.Base.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {
            InitializeComponent();
        }

        private void GetCode(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(this, Literals.Code, result.Text));
        }
    }
}