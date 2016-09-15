using Android.App;
using Android.OS;
using Android.Content.PM;
using Xamarin.Forms.Platform.Android;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Presenter.Droid;
using MvvmCross.Core.ViewModels;

namespace Xamarin.VisUITest.App.Droid
{
    [Activity(Label = "Xamarin.VisUITest.App.Droid",
              ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Forms.Init(this, bundle);

            MvxFormsApp mvxFormsApp = new MvxFormsApp();
            LoadApplication(mvxFormsApp);
            App.InitializeResources();

            MvxFormsDroidPagePresenter presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsDroidPagePresenter;
            presenter.MvxFormsApp = mvxFormsApp;

            Mvx.Resolve<IMvxAppStart>()
               .Start();
        }
    }
}