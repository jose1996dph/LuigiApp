
using Android.App;
using Android.Content;
using Android.OS;

namespace LuigiApp.Droid
{
    [Activity(Label = "Luigi's App", Icon = "@mipmap/icon", MainLauncher = true, NoHistory = true, Theme = "@style/MyTheme.Splash")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            Finish();
        }
    }
}