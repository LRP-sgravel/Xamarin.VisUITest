using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Droid.Views;
using Xamarin.Forms;

namespace Xamarin.VisUITest.App.Droid
{
    [Activity(Label = "Xamarin.VisUITest.App.Droid",
        MainLauncher = true,
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme.Base",
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen() : base(Resource.Layout.SplashScreen)
        {
        }

        private bool _isInitializationComplete = false;
        public override void InitializationComplete()
        {
            if (!_isInitializationComplete)
            {
                _isInitializationComplete = true;
                StartActivity(typeof(MainActivity));
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            Forms.Forms.Init(this, bundle);

            // Leverage controls' StyleId attrib. to Xamarin.UITest
            Forms.Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId))
                {
                    e.NativeView.ContentDescription = e.View.StyleId;
                }
            };

            base.OnCreate(bundle);
        }
    }
}